using ColossalFramework;
using ColossalFramework.Math;
using System;
using UnityEngine;

namespace CustomizeIt.AI
{
    public class SharedAI
    {
        internal static void EnsureCitizenUnits(ushort buildingID, BuildingInfo m_info, ref Building data, int homeCount, int workCount, int visitCount, int studentCount) {
            var instance = Singleton<CitizenManager>.instance;
            var unitBuffer = instance.m_units.m_buffer;
            var citizenBuffer = instance.m_citizens.m_buffer;

            int totalWorkCount = (workCount + 4) / 5;
            int totalVisitCount = (visitCount + 4) / 5;
            int totalHomeCount = homeCount;
            int[] workersRequired = new int[] { 0, 0, 0, 0 };

            if ((data.m_flags & (Building.Flags.Abandoned | Building.Flags.Collapsed)) == Building.Flags.None) {
                Citizen.Wealth wealthLevel = Citizen.GetWealthLevel(m_info.m_class.m_level);
                uint num = 0u;
                uint num2 = data.m_citizenUnits;
                int num3 = 0;
                while (num2 != 0u) {
                    CitizenUnit.Flags flags = instance.m_units.m_buffer[(int)((UIntPtr)num2)].m_flags;
                    if ((ushort)(flags & CitizenUnit.Flags.Home) != 0) {
                        instance.m_units.m_buffer[(int)((UIntPtr)num2)].SetWealthLevel(wealthLevel);
                        homeCount--;
                    }
                    if ((ushort)(flags & CitizenUnit.Flags.Work) != 0) {
                        workCount -= 5;
                        for (int i = 0; i < 5; i++) {
                            uint citizen = unitBuffer[(int)((UIntPtr)num2)].GetCitizen(i);
                            if (citizen != 0u) {
                                workersRequired[(int)citizenBuffer[(int)((UIntPtr)citizen)].EducationLevel]--;
                            }
                        }
                    }
                    if ((ushort)(flags & CitizenUnit.Flags.Visit) != 0) {
                        visitCount -= 5;
                    }
                    if ((ushort)(flags & CitizenUnit.Flags.Student) != 0) {
                        studentCount -= 5;
                    }
                    num = num2;
                    num2 = instance.m_units.m_buffer[(int)((UIntPtr)num2)].m_nextUnit;
                    if (++num3 > 524288) {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }

                studentCount = Mathf.Max(0, studentCount);

                if (homeCount > 0 || workCount > 0 || visitCount > 0 || studentCount > 0) {
                    if (instance.CreateUnits(out uint num4, ref Singleton<SimulationManager>.instance.m_randomizer, buildingID, 0, homeCount, workCount, visitCount, 0, studentCount)) {
                        if (num != 0u) {
                            instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit = num4;
                        } else {
                            data.m_citizenUnits = num4;
                        }
                    }
                }

                TransferManager.TransferOffer offer = default;
                offer.Building = buildingID;
                Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Worker0, offer);
                Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Worker1, offer);
                Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Worker2, offer);
                Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Worker3, offer);

                ((PrivateBuildingAI)m_info.m_buildingAI).CalculateWorkplaceCount(m_info.m_class.m_level, new Randomizer((int)buildingID), data.Width, data.Length, out int worker0, out int worker1, out int worker2, out int worker3);

                workersRequired[0] += worker0;
                workersRequired[1] += worker1;
                workersRequired[2] += worker2;
                workersRequired[3] += worker3;

                if (workCount < 0) {
                    RemoveWorkers(buildingID, ref data, totalWorkCount);
                    PromoteWorkers(buildingID, ref data, ref workersRequired);
                } else if (homeCount < 0) {
                    RemoveHouseholds(buildingID, ref data, totalHomeCount);
                }
                if (visitCount < 0) {
                    RemoveVisitors(buildingID, ref data, totalVisitCount);
                }
            }
        }

        private static void PromoteWorkers(ushort buildingID, ref Building data, ref int[] workersRequired) {
            if (workersRequired[0] == 0 && workersRequired[1] == 0 && workersRequired[2] == 0 && workersRequired[3] == 0) return;

            var instance = Singleton<CitizenManager>.instance;
            var unitBuffer = instance.m_units.m_buffer;
            var citizenBuffer = instance.m_citizens.m_buffer;

            int loopCounter = 0;
            uint previousUnit = data.m_citizenUnits;
            uint currentUnit = data.m_citizenUnits;
            while (currentUnit != 0u) {
                uint nextUnit = unitBuffer[currentUnit].m_nextUnit;

                if ((unitBuffer[currentUnit].m_flags & CitizenUnit.Flags.Work) != 0) {
                    for (int i = 0; i < 5; i++) {
                        uint citizenID = unitBuffer[(int)((UIntPtr)currentUnit)].GetCitizen(i);
                        if (citizenID != 0u) {
                            int citizenIndex = (int)((UIntPtr)citizenID);
                            ushort citizenInstanceIndex = citizenBuffer[citizenIndex].m_instance;

                            Citizen citizen = citizenBuffer[(int)((UIntPtr)citizenID)];
                            int education = (int)citizen.EducationLevel;

                            if ((citizen.EducationLevel != Citizen.Education.ThreeSchools) && (workersRequired[education] < 0 && workersRequired[education + 1] > 0)) {
                                int number = Singleton<SimulationManager>.instance.m_randomizer.Int32(0, 100) - (education * 15);
                                if (number > 50) {
                                    if (citizen.EducationLevel == Citizen.Education.Uneducated) {
                                        citizen.Education1 = true;
                                        workersRequired[0]++;
                                        workersRequired[1]--;
                                    } else if (citizen.EducationLevel == Citizen.Education.OneSchool) {
                                        citizen.Education2 = true;
                                        workersRequired[1]++;
                                        workersRequired[2]--;
                                    } else if (citizen.EducationLevel == Citizen.Education.TwoSchools) {
                                        citizen.Education3 = true;
                                        workersRequired[2]++;
                                        workersRequired[3]--;
                                    }
                                } else {
                                    workersRequired[education]++;
                                    citizenBuffer[(int)((UIntPtr)citizenID)].m_workBuilding = 0;
                                    RemoveCitizenFromUnit(currentUnit, i);
                                }
                            } else if (workersRequired[education] < 0) {
                                workersRequired[education]++;
                                citizenBuffer[(int)((UIntPtr)citizenID)].m_workBuilding = 0;
                                RemoveCitizenFromUnit(currentUnit, i);
                            }
                        }
                    }
                }

                previousUnit = currentUnit;
                currentUnit = nextUnit;

                if (++loopCounter > 524288) {
                    currentUnit = 0u;
                }
            }
        }


        private static void RemoveCitizenFromUnit(uint unit, int citizen) {
            var unitBuffer = Singleton<CitizenManager>.instance.m_units.m_buffer;

            switch (citizen) {
                case 0:
                    unitBuffer[(int)((UIntPtr)unit)].m_citizen0 = 0u;
                    break;
                case 1:
                    unitBuffer[(int)((UIntPtr)unit)].m_citizen1 = 0u;
                    break;
                case 2:
                    unitBuffer[(int)((UIntPtr)unit)].m_citizen2 = 0u;
                    break;
                case 3:
                    unitBuffer[(int)((UIntPtr)unit)].m_citizen3 = 0u;
                    break;
                case 4:
                    unitBuffer[(int)((UIntPtr)unit)].m_citizen4 = 0u;
                    break;
            }
        }

        private static void RemoveHouseholds(ushort buildingID, ref Building data, int maxHomes) {
            var instance = Singleton<CitizenManager>.instance;
            var unitBuffer = instance.m_units.m_buffer;
            var citizenBuffer = instance.m_citizens.m_buffer;

            int loopCounter = 0;
            uint previousUnit = data.m_citizenUnits;
            uint currentUnit = data.m_citizenUnits;

            while (currentUnit != 0u) {
                uint nextUnit = unitBuffer[currentUnit].m_nextUnit;
                bool removeCurrentUnit = false;

                if ((unitBuffer[currentUnit].m_flags & CitizenUnit.Flags.Home) != 0) {
                    if (maxHomes > 0) {
                        maxHomes--;
                    } else {
                        for (int i = 0; i < 5; i++) {
                            uint citizen = unitBuffer[(int)((UIntPtr)currentUnit)].GetCitizen(i);
                            citizenBuffer[(int)((UIntPtr)citizen)].m_homeBuilding = 0;
                        }
                        removeCurrentUnit = true;
                    }
                }

                if (removeCurrentUnit) {
                    unitBuffer[previousUnit].m_nextUnit = nextUnit;

                    unitBuffer[currentUnit] = default;
                    instance.m_units.ReleaseItem(currentUnit);
                } else {
                    previousUnit = currentUnit;
                }
                currentUnit = nextUnit;

                if (++loopCounter > 524288) {
                    currentUnit = 0u;
                }
            }

            data.m_crimeBuffer = 0;
            data.m_garbageBuffer = 0;
            data.m_sewageBuffer = 0;
        }

        private static void RemoveWorkers(ushort buildingID, ref Building data, int workerUnits) {
            var instance = Singleton<CitizenManager>.instance;
            var unitBuffer = instance.m_units.m_buffer;
            var citizenBuffer = instance.m_citizens.m_buffer;

            int loopCounter = 0;
            uint previousUnit = data.m_citizenUnits;
            uint currentUnit = data.m_citizenUnits;


            while (currentUnit != 0u) {
                uint nextUnit = unitBuffer[currentUnit].m_nextUnit;
                bool removeCurrentUnit = false;

                if ((unitBuffer[currentUnit].m_flags & CitizenUnit.Flags.Work) != 0) {
                    if (workerUnits > 0) {
                        workerUnits--;
                    } else {
                        for (int i = 0; i < 5; i++) {
                            uint citizen = unitBuffer[(int)((UIntPtr)currentUnit)].GetCitizen(i);
                            if (citizen != 0u) {
                                citizenBuffer[(int)((UIntPtr)citizen)].m_workBuilding = 0;
                            }
                        }
                        removeCurrentUnit = true;
                    }
                }

                if (removeCurrentUnit) {
                    unitBuffer[previousUnit].m_nextUnit = nextUnit;

                    unitBuffer[currentUnit] = default;
                    instance.m_units.ReleaseItem(currentUnit);
                } else {
                    previousUnit = currentUnit;
                }
                currentUnit = nextUnit;

                if (++loopCounter > 524288) {
                    currentUnit = 0u;
                }
            }

            data.m_crimeBuffer = 0;
            data.m_garbageBuffer = 0;
            data.m_sewageBuffer = 0;
        }

        private static void RemoveVisitors(ushort buildingID, ref Building data, int visitorsUnit) {
            var instance = Singleton<CitizenManager>.instance;
            var unitBuffer = instance.m_units.m_buffer;
            var citizenBuffer = instance.m_citizens.m_buffer;

            int loopCounter = 0;
            uint previousUnit = data.m_citizenUnits;
            uint currentUnit = data.m_citizenUnits;

            while (currentUnit != 0u) {
                uint nextUnit = unitBuffer[currentUnit].m_nextUnit;
                bool removeCurrentUnit = false;

                if ((unitBuffer[currentUnit].m_flags & CitizenUnit.Flags.Visit) != 0) {
                    if (visitorsUnit > 0) {
                        visitorsUnit--;
                    } else {
                        for (int i = 0; i < 5; i++) {
                            uint citizen = unitBuffer[(int)((UIntPtr)currentUnit)].GetCitizen(i);
                            citizenBuffer[(int)((UIntPtr)citizen)].m_visitBuilding = 0;
                        }
                        removeCurrentUnit = true;
                    }
                }

                if (removeCurrentUnit) {
                    unitBuffer[previousUnit].m_nextUnit = nextUnit;

                    unitBuffer[currentUnit] = default;
                    instance.m_units.ReleaseItem(currentUnit);
                } else {
                    previousUnit = currentUnit;
                }
                currentUnit = nextUnit;

                if (++loopCounter > 524288) {
                    currentUnit = 0u;
                }
            }
        }
    }
}

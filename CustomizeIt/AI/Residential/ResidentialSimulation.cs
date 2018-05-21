using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Residential
{
    public partial class CustomizableResidentialBuildingAI : ResidentialBuildingAI, ICustomAI
    {
        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
            int num = 0;
            int citizenCount = 0;
            int num2 = 0;
            int aliveHomeCount = 0;
            int num3 = 0;
            GetHomeBehaviour(buildingID, ref buildingData, ref behaviourData, ref num, ref citizenCount, ref num2, ref aliveHomeCount, ref num3);
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(buildingData.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[district].m_servicePolicies;
            DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[district].m_taxationPolicies;
            DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[district].m_cityPlanningPolicies;
            District[] buffer = instance.m_districts.m_buffer;
            byte b = district;
            buffer[b].m_servicePoliciesEffect = (buffer[b].m_servicePoliciesEffect | (servicePolicies & (DistrictPolicies.Services.PowerSaving | DistrictPolicies.Services.WaterSaving | DistrictPolicies.Services.SmokeDetectors | DistrictPolicies.Services.PetBan | DistrictPolicies.Services.Recycling | DistrictPolicies.Services.SmokingBan | DistrictPolicies.Services.ExtraInsulation | DistrictPolicies.Services.NoElectricity | DistrictPolicies.Services.OnlyElectricity)));
            if (m_info.m_class.m_subService == ItemClass.SubService.ResidentialLow || m_info.m_class.m_subService == ItemClass.SubService.ResidentialLowEco)
            {
                if ((taxationPolicies & (DistrictPolicies.Taxation.TaxRaiseResLow | DistrictPolicies.Taxation.TaxLowerResLow)) != (DistrictPolicies.Taxation.TaxRaiseResLow | DistrictPolicies.Taxation.TaxLowerResLow))
                {
                    District[] buffer2 = instance.m_districts.m_buffer;
                    byte b2 = district;
                    buffer2[b2].m_taxationPoliciesEffect = (buffer2[b2].m_taxationPoliciesEffect | (taxationPolicies & (DistrictPolicies.Taxation.TaxRaiseResLow | DistrictPolicies.Taxation.TaxLowerResLow)));
                }
            }
            else if ((taxationPolicies & (DistrictPolicies.Taxation.TaxRaiseResHigh | DistrictPolicies.Taxation.TaxLowerResHigh)) != (DistrictPolicies.Taxation.TaxRaiseResHigh | DistrictPolicies.Taxation.TaxLowerResHigh))
            {
                District[] buffer3 = instance.m_districts.m_buffer;
                byte b3 = district;
                buffer3[b3].m_taxationPoliciesEffect = (buffer3[b3].m_taxationPoliciesEffect | (taxationPolicies & (DistrictPolicies.Taxation.TaxRaiseResHigh | DistrictPolicies.Taxation.TaxLowerResHigh)));
            }
            District[] buffer4 = instance.m_districts.m_buffer;
            byte b4 = district;
            buffer4[b4].m_cityPlanningPoliciesEffect = (buffer4[b4].m_cityPlanningPoliciesEffect | (cityPlanningPolicies & (DistrictPolicies.CityPlanning.HighTechHousing | DistrictPolicies.CityPlanning.HeavyTrafficBan | DistrictPolicies.CityPlanning.EncourageBiking | DistrictPolicies.CityPlanning.BikeBan | DistrictPolicies.CityPlanning.OldTown | DistrictPolicies.CityPlanning.AntiSlip | DistrictPolicies.CityPlanning.LightningRods | DistrictPolicies.CityPlanning.VIPArea | DistrictPolicies.CityPlanning.CombustionEngineBan | DistrictPolicies.CityPlanning.ElectricCars)));
            GetConsumptionRates(new Randomizer(buildingID), 100, out int num4, out int num5, out int num6, out int num7, out int num8);
            if ((buildingData.m_flags & Building.Flags.Evacuating) != Building.Flags.None)
            {
                num4 = 0;
                num5 = 0;
                num6 = 0;
                num7 = 0;
                num8 = 0;
            }
            else
            {
                num4 = (num4 * behaviourData.m_electricityConsumption + 9999) / 10000;// removed 1 +
                num5 = (num5 * behaviourData.m_waterConsumption + 9999) / 10000;// removed 1 +
                num6 = (num6 * behaviourData.m_sewageAccumulation + 9999) / 10000;// removed 1 +
                num7 = (num7 * behaviourData.m_garbageAccumulation + 9999) / 10000;
                num8 = (num8 * behaviourData.m_incomeAccumulation + 9999) / 10000;
            }
            int heatingConsumption = 0;
            if (num4 != 0 && instance.IsPolicyLoaded(DistrictPolicies.Policies.ExtraInsulation))
            {
                if ((servicePolicies & DistrictPolicies.Services.ExtraInsulation) != DistrictPolicies.Services.None)
                {
                    heatingConsumption = Mathf.Max(1, num4 * 3 + 8 >> 4);
                    num8 = num8 * 95 / 100;
                }
                else
                {
                    heatingConsumption = Mathf.Max(1, num4 + 2 >> 2);
                }
            }
            if (num7 != 0)
            {
                if ((servicePolicies & DistrictPolicies.Services.Recycling) != DistrictPolicies.Services.None)
                {
                    if ((servicePolicies & DistrictPolicies.Services.PetBan) != DistrictPolicies.Services.None)
                    {
                        num7 = Mathf.Max(1, num7 * 7650 / 10000);
                    }
                    else
                    {
                        num7 = Mathf.Max(1, num7 * 85 / 100);
                    }
                    num8 = num8 * 95 / 100;
                }
                else if ((servicePolicies & DistrictPolicies.Services.PetBan) != DistrictPolicies.Services.None)
                {
                    num7 = Mathf.Max(1, num7 * 90 / 100);
                }
            }
            if (buildingData.m_fireIntensity == 0)
            {
                int num9 = HandleCommonConsumption(buildingID, ref buildingData, ref frameData, ref num4, ref heatingConsumption, ref num5, ref num6, ref num7, servicePolicies);
                num8 = (num8 * num9 + 99) / 100;
                if (num8 != 0)
                {
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PrivateIncome, num8, m_info.m_class, taxationPolicies);
                }
                buildingData.m_flags |= Building.Flags.Active;
            }
            else
            {
                num4 = 0;
                heatingConsumption = 0;
                num5 = 0;
                num6 = 0;
                num7 = 0;
                buildingData.m_problems = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.Electricity | Notification.Problem.Water | Notification.Problem.Sewage | Notification.Problem.Flood | Notification.Problem.Heating);
                buildingData.m_flags &= ~Building.Flags.Active;
            }
            int num10 = 0;
            int wellbeing = 0;
            float radius = (buildingData.Width + buildingData.Length) * 2.5f;
            if (behaviourData.m_healthAccumulation != 0)
            {
                if (num != 0)
                {
                    num10 = (behaviourData.m_healthAccumulation + (num >> 1)) / num;
                }
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.Health, behaviourData.m_healthAccumulation, buildingData.m_position, radius);
            }
            if (behaviourData.m_wellbeingAccumulation != 0)
            {
                if (num != 0)
                {
                    wellbeing = (behaviourData.m_wellbeingAccumulation + (num >> 1)) / num;
                }
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.Wellbeing, behaviourData.m_wellbeingAccumulation, buildingData.m_position, radius);
            }
            int taxRate = Singleton<EconomyManager>.instance.GetTaxRate(m_info.m_class, taxationPolicies);
            int num11 = ((Citizen.Wealth)11 - Citizen.GetWealthLevel(m_info.m_class.m_level));
            if (m_info.m_class.m_subService == ItemClass.SubService.ResidentialHigh)
            {
                num11++;
            }
            if (taxRate >= num11 + 4)
            {
                if (buildingData.m_taxProblemTimer != 0 || Singleton<SimulationManager>.instance.m_randomizer.Int32(32u) == 0)
                {
                    int num12 = taxRate - num11 >> 2;
                    buildingData.m_taxProblemTimer = (byte)Mathf.Min(255, buildingData.m_taxProblemTimer + num12);
                    if (buildingData.m_taxProblemTimer >= 96)
                    {
                        buildingData.m_problems = Notification.AddProblems(buildingData.m_problems, Notification.Problem.TaxesTooHigh | Notification.Problem.MajorProblem);
                    }
                    else if (buildingData.m_taxProblemTimer >= 32)
                    {
                        buildingData.m_problems = Notification.AddProblems(buildingData.m_problems, Notification.Problem.TaxesTooHigh);
                    }
                }
            }
            else
            {
                buildingData.m_taxProblemTimer = (byte)Mathf.Max(0, (buildingData.m_taxProblemTimer - 1));
                buildingData.m_problems = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.TaxesTooHigh);
            }
            int num13 = Citizen.GetHappiness(num10, wellbeing);
            if ((buildingData.m_problems & Notification.Problem.MajorProblem) != Notification.Problem.None)
            {
                num13 -= num13 >> 1;
            }
            else if (buildingData.m_problems != Notification.Problem.None)
            {
                num13 -= num13 >> 2;
            }
            buildingData.m_health = (byte)num10;
            buildingData.m_happiness = (byte)num13;
            buildingData.m_citizenCount = (byte)num;
            buildingData.m_education1 = (byte)behaviourData.m_education1Count;
            buildingData.m_education2 = (byte)behaviourData.m_education2Count;
            buildingData.m_education3 = (byte)behaviourData.m_education3Count;
            buildingData.m_teens = (byte)behaviourData.m_teenCount;
            buildingData.m_youngs = (byte)behaviourData.m_youngCount;
            buildingData.m_adults = (byte)behaviourData.m_adultCount;
            buildingData.m_seniors = (byte)behaviourData.m_seniorCount;
            HandleSick(buildingID, ref buildingData, ref behaviourData, citizenCount);
            HandleDead(buildingID, ref buildingData, ref behaviourData, citizenCount);
            int num14 = behaviourData.m_crimeAccumulation / 10;
            if ((servicePolicies & DistrictPolicies.Services.RecreationalUse) != DistrictPolicies.Services.None)
            {
                num14 = num14 * 3 + 3 >> 2;
            }
            HandleCrime(buildingID, ref buildingData, num14, num);
            int num15 = buildingData.m_crimeBuffer;
            if (num != 0)
            {
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.Density, num, buildingData.m_position, radius);
                int num16 = behaviourData.m_educated0Count * 30 + behaviourData.m_educated1Count * 15 + behaviourData.m_educated2Count * 10;
                num16 = num16 / num + 50;
                if (buildingData.m_crimeBuffer > num * 40)
                {
                    num16 += 30;
                }
                else if (buildingData.m_crimeBuffer > num * 15)
                {
                    num16 += 15;
                }
                else if (buildingData.m_crimeBuffer > num * 5)
                {
                    num16 += 10;
                }
                buildingData.m_fireHazard = (byte)num16;
                num15 = (num15 + (num >> 1)) / num;
            }
            else
            {
                buildingData.m_fireHazard = 0;
                num15 = 0;
            }
            int num17 = 0;
            if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.HighTechHousing) != DistrictPolicies.CityPlanning.None)
            {
                num17 += 25;
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.LandValue, 50, buildingData.m_position, radius);
            }
            if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.AntiSlip) != DistrictPolicies.CityPlanning.None)
            {
                num17 += behaviourData.m_seniorCount << 1;
            }
            if (num17 != 0)
            {
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, num17, m_info.m_class);
            }
            SimulationManager instance2 = Singleton<SimulationManager>.instance;
            uint num18 = (instance2.m_currentFrameIndex & 3840u) >> 8;
            if (num18 == (ulong)(buildingID & 15) && Singleton<ZoneManager>.instance.m_lastBuildIndex == instance2.m_currentBuildIndex && (buildingData.m_flags & Building.Flags.Upgrading) == Building.Flags.None)
            {
                CheckBuildingLevel(buildingID, ref buildingData, ref frameData, ref behaviourData);
            }
            if ((buildingData.m_flags & (Building.Flags.Completed | Building.Flags.Upgrading)) != Building.Flags.None)
            {
                if (num3 != 0 && (buildingData.m_problems & Notification.Problem.MajorProblem) == Notification.Problem.None && Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0)
                {
                    TransferManager.TransferReason homeReason = GetHomeReason(buildingID, ref buildingData, ref Singleton<SimulationManager>.instance.m_randomizer);
                    if (homeReason != TransferManager.TransferReason.None)
                    {
                        TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                        offer.Priority = Mathf.Max(1, num3 * 8 / num2);
                        offer.Building = buildingID;
                        offer.Position = buildingData.m_position;
                        offer.Amount = num3;
                        Singleton<TransferManager>.instance.AddIncomingOffer(homeReason, offer);
                    }
                }
                instance.m_districts.m_buffer[district].AddResidentialData(ref behaviourData, num, num10, num13, num15, num2, aliveHomeCount, num3, (int)m_info.m_class.m_level, num4, heatingConsumption, num5, num6, num7, num8, Mathf.Min(100, (buildingData.m_garbageBuffer / 50)), (buildingData.m_waterPollution * 100 / byte.MaxValue), m_info.m_class.m_subService);
                PrivateBuildingAISimulationStepActive(buildingID, ref buildingData, ref frameData);
                HandleFire(buildingID, ref buildingData, ref frameData, servicePolicies);
            }
            if (m_isPloppable) buildingData.FixFlags();
        }

        private void PrivateBuildingAISimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            CommonBuildingAISimulationStepActive(buildingID, ref buildingData, ref frameData);
            if ((buildingData.m_problems & Notification.Problem.MajorProblem) != Notification.Problem.None)
            {
                if (buildingData.m_fireIntensity == 0)
                {
                    buildingData.m_majorProblemTimer = (byte)Mathf.Min(255, buildingData.m_majorProblemTimer + 1);
                    if (buildingData.m_majorProblemTimer >= 64 && !Singleton<BuildingManager>.instance.m_abandonmentDisabled)
                    {
                        if ((buildingData.m_flags & Building.Flags.Flooded) != Building.Flags.None)
                        {
                            InstanceID id = default(InstanceID);
                            id.Building = buildingID;
                            Singleton<InstanceManager>.instance.SetGroup(id, null);
                            buildingData.m_flags &= ~Building.Flags.Flooded;
                        }
                        buildingData.m_majorProblemTimer = 192;
                        buildingData.m_flags &= ~Building.Flags.Active;
                        buildingData.m_flags |= Building.Flags.Abandoned;
                        buildingData.m_problems = (Notification.Problem.FatalProblem | (buildingData.m_problems & ~Notification.Problem.MajorProblem));
                        RemovePeople(buildingID, ref buildingData, 100);
                        BuildingDeactivated(buildingID, ref buildingData);
                        Singleton<BuildingManager>.instance.UpdateBuildingRenderer(buildingID, true);
                    }
                }
            }
            else
            {
                buildingData.m_majorProblemTimer = 0;
            }
        }

        private void CommonBuildingAISimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            Notification.Problem problem = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.Garbage);
            if (buildingData.m_garbageBuffer >= 2000)
            {
                int num = buildingData.m_garbageBuffer / 1000;
                if (Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0)
                {
                    Singleton<NaturalResourceManager>.instance.TryDumpResource(NaturalResourceManager.Resource.Pollution, num, num, buildingData.m_position, 0f);
                }
                if (num >= 3)
                {
                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                    {
                        if (num >= 6)
                        {
                            problem = Notification.AddProblems(problem, Notification.Problem.Garbage | Notification.Problem.MajorProblem);
                        }
                        else
                        {
                            problem = Notification.AddProblems(problem, Notification.Problem.Garbage);
                        }
                        GuideController properties = Singleton<GuideManager>.instance.m_properties;
                        if (properties != null)
                        {
                            int publicServiceIndex = ItemClass.GetPublicServiceIndex(ItemClass.Service.Garbage);
                            Singleton<GuideManager>.instance.m_serviceNeeded[publicServiceIndex].Activate(properties.m_serviceNeeded, ItemClass.Service.Garbage);
                        }
                    }
                    else
                    {
                        buildingData.m_garbageBuffer = 2000;
                    }
                }
            }
            buildingData.m_problems = problem;
            float radius = (buildingData.Width + buildingData.Length) * 2.5f;
            if (buildingData.m_crimeBuffer != 0)
            {
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.CrimeRate, buildingData.m_crimeBuffer, buildingData.m_position, radius);
            }
            if (GetFireParameters(buildingID, ref buildingData, out int num2, out int num3, out int num4))
            {
                DistrictManager instance = Singleton<DistrictManager>.instance;
                byte district = instance.GetDistrict(buildingData.m_position);
                DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[district].m_servicePolicies;
                DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[district].m_cityPlanningPolicies;
                if ((servicePolicies & DistrictPolicies.Services.SmokeDetectors) != DistrictPolicies.Services.None)
                {
                    num2 = num2 * 75 / 100;
                }
                if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.LightningRods) != DistrictPolicies.CityPlanning.None)
                {
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, 10, m_info.m_class);
                }
            }
            num2 = 100 - (10 + num4) * 50000 / ((100 + num2) * (100 + num3));
            if (num2 > 0)
            {
                num2 = num2 * buildingData.Width * buildingData.Length;
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.FireHazard, num2, buildingData.m_position, radius);
            }
            Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.FirewatchCoverage, 50, buildingData.m_position, 100f);
            if (Singleton<DisasterManager>.instance.IsEvacuating(buildingData.m_position))
            {
                if ((buildingData.m_flags & Building.Flags.Evacuating) == Building.Flags.None && CanEvacuate())
                {
                    Singleton<ImmaterialResourceManager>.instance.CheckLocalResource(ImmaterialResourceManager.Resource.RadioCoverage, buildingData.m_position, out int num5);
                    if (Singleton<SimulationManager>.instance.m_randomizer.Int32(100u) < num5 + 10)
                    {
                        SetEvacuating(buildingID, ref buildingData, true);
                    }
                }
            }
            else if ((buildingData.m_flags & Building.Flags.Evacuating) != Building.Flags.None)
            {
                SetEvacuating(buildingID, ref buildingData, false);
            }
        }
        private TransferManager.TransferReason GetHomeReason(ushort buildingID, ref Building buildingData, ref Randomizer r)
        {
            if ((m_info.m_class.m_subService == ItemClass.SubService.ResidentialLow || m_info.m_class.m_subService == ItemClass.SubService.ResidentialLowEco) == (r.Int32(10u) != 0))
            {
                switch (m_info.m_class.m_level)
                {
                    case ItemClass.Level.Level1:
                        return TransferManager.TransferReason.Family0;
                    case ItemClass.Level.Level2:
                        return TransferManager.TransferReason.Family1;
                    case ItemClass.Level.Level3:
                        return TransferManager.TransferReason.Family2;
                    case ItemClass.Level.Level4:
                        return TransferManager.TransferReason.Family3;
                    case ItemClass.Level.Level5:
                        return TransferManager.TransferReason.Family3;
                    default:
                        return TransferManager.TransferReason.Family0;
                }
            }
            else if (r.Int32(2u) == 0)
            {
                switch (m_info.m_class.m_level)
                {
                    case ItemClass.Level.Level1:
                        return TransferManager.TransferReason.Single0;
                    case ItemClass.Level.Level2:
                        return TransferManager.TransferReason.Single1;
                    case ItemClass.Level.Level3:
                        return TransferManager.TransferReason.Single2;
                    case ItemClass.Level.Level4:
                        return TransferManager.TransferReason.Single3;
                    case ItemClass.Level.Level5:
                        return TransferManager.TransferReason.Single3;
                    default:
                        return TransferManager.TransferReason.Single0;
                }
            }
            else
            {
                switch (m_info.m_class.m_level)
                {
                    case ItemClass.Level.Level1:
                        return TransferManager.TransferReason.Single0B;
                    case ItemClass.Level.Level2:
                        return TransferManager.TransferReason.Single1B;
                    case ItemClass.Level.Level3:
                        return TransferManager.TransferReason.Single2B;
                    case ItemClass.Level.Level4:
                        return TransferManager.TransferReason.Single3B;
                    case ItemClass.Level.Level5:
                        return TransferManager.TransferReason.Single3B;
                    default:
                        return TransferManager.TransferReason.Single0B;
                }
            }
        }

        private void CheckBuildingLevel(ushort buildingID, ref Building buildingData, ref Building.Frame frameData, ref Citizen.BehaviourData behaviour)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(buildingData.m_position);
            DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[district].m_cityPlanningPolicies;
            int num = behaviour.m_educated1Count + behaviour.m_educated2Count * 2 + behaviour.m_educated3Count * 3;
            int num2 = behaviour.m_teenCount + behaviour.m_youngCount * 2 + behaviour.m_adultCount * 3 + behaviour.m_seniorCount * 3;
            int averageEducation;
            ItemClass.Level level;
            int num3;
            if (num2 != 0)
            {
                averageEducation = (num * 300 + (num2 >> 1)) / num2;
                num = (num * 72 + (num2 >> 1)) / num2;
                if (num < 15)
                {
                    level = ItemClass.Level.Level1;
                    num3 = 1 + num;
                }
                else if (num < 30)
                {
                    level = ItemClass.Level.Level2;
                    num3 = 1 + (num - 15);
                }
                else if (num < 45)
                {
                    level = ItemClass.Level.Level3;
                    num3 = 1 + (num - 30);
                }
                else if (num < 60)
                {
                    level = ItemClass.Level.Level4;
                    num3 = 1 + (num - 45);
                }
                else
                {
                    level = ItemClass.Level.Level5;
                    num3 = 1;
                }
                if (level < m_info.m_class.m_level)
                {
                    num3 = 1;
                }
                else if (level > m_info.m_class.m_level)
                {
                    num3 = 15;
                }
            }
            else
            {
                level = ItemClass.Level.Level1;
                averageEducation = 0;
                num3 = 0;
            }
            Singleton<ImmaterialResourceManager>.instance.CheckLocalResource(ImmaterialResourceManager.Resource.LandValue, buildingData.m_position, out int num4);
            ItemClass.Level level2;
            int num5;
            if (num3 != 0)
            {
                if (num4 < 6)
                {
                    level2 = ItemClass.Level.Level1;
                    num5 = 1 + (num4 * 15 + 3) / 6;
                }
                else if (num4 < 21)
                {
                    level2 = ItemClass.Level.Level2;
                    num5 = 1 + ((num4 - 6) * 15 + 7) / 15;
                }
                else if (num4 < 41)
                {
                    level2 = ItemClass.Level.Level3;
                    num5 = 1 + ((num4 - 21) * 15 + 10) / 20;
                }
                else if (num4 < 61)
                {
                    level2 = ItemClass.Level.Level4;
                    num5 = 1 + ((num4 - 41) * 15 + 10) / 20;
                }
                else
                {
                    level2 = ItemClass.Level.Level5;
                    num5 = 1;
                }
                if (level2 < m_info.m_class.m_level)
                {
                    num5 = 1;
                }
                else if (level2 > m_info.m_class.m_level)
                {
                    num5 = 15;
                }
            }
            else
            {
                level2 = ItemClass.Level.Level1;
                num5 = 0;
            }
            bool flag = false;
            if (m_info.m_class.m_level == ItemClass.Level.Level2)
            {
                if (num4 == 0)
                {
                    flag = true;
                }
            }
            else if (m_info.m_class.m_level == ItemClass.Level.Level3)
            {
                if (num4 < 11)
                {
                    flag = true;
                }
            }
            else if (m_info.m_class.m_level == ItemClass.Level.Level4)
            {
                if (num4 < 31)
                {
                    flag = true;
                }
            }
            else if (m_info.m_class.m_level == ItemClass.Level.Level5 && num4 < 51)
            {
                flag = true;
            }
            ItemClass.Level level3 = (ItemClass.Level)Mathf.Min((int)level, (int)level2);
            Singleton<BuildingManager>.instance.m_LevelUpWrapper.OnCalculateResidentialLevelUp(ref level3, ref num3, ref num5, ref flag, averageEducation, num4, buildingID, m_info.m_class.m_service, m_info.m_class.m_subService, m_info.m_class.m_level);
            if (flag)
            {
                buildingData.m_serviceProblemTimer = (byte)Mathf.Min(255, buildingData.m_serviceProblemTimer + 1);
                if (buildingData.m_serviceProblemTimer >= 8)
                {
                    buildingData.m_problems = Notification.AddProblems(buildingData.m_problems, Notification.Problem.LandValueLow | Notification.Problem.MajorProblem);
                }
                else if (buildingData.m_serviceProblemTimer >= 4)
                {
                    buildingData.m_problems = Notification.AddProblems(buildingData.m_problems, Notification.Problem.LandValueLow);
                }
                else
                {
                    buildingData.m_problems = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.LandValueLow);
                }
            }
            else
            {
                buildingData.m_serviceProblemTimer = 0;
                buildingData.m_problems = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.LandValueLow);
            }
            if (level3 > m_info.m_class.m_level)
            {
                num3 = 0;
                num5 = 0;
                if ((m_info.m_class.m_subService == ItemClass.SubService.ResidentialHigh || m_info.m_class.m_subService == ItemClass.SubService.ResidentialHighEco) && (cityPlanningPolicies & DistrictPolicies.CityPlanning.HighriseBan) != DistrictPolicies.CityPlanning.None && level3 == ItemClass.Level.Level5)
                {
                    District[] buffer = instance.m_districts.m_buffer;
                    byte b = district;
                    buffer[b].m_cityPlanningPoliciesEffect = (buffer[b].m_cityPlanningPoliciesEffect | DistrictPolicies.CityPlanning.HighriseBan);
                    level3 = ItemClass.Level.Level4;
                    num3 = 1;
                }
                if (buildingData.m_problems == Notification.Problem.None && level3 > m_info.m_class.m_level && GetUpgradeInfo(buildingID, ref buildingData) != null && !Singleton<DisasterManager>.instance.IsEvacuating(buildingData.m_position))
                {
                    frameData.m_constructState = 0;
                    StartUpgrading(buildingID, ref buildingData);
                }
            }
            buildingData.m_levelUpProgress = (byte)(num3 | num5 << 4);
        }
    }
}

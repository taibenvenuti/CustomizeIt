using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Commercial
{
    public partial class CustomizableCommercialBuildingAI : CommercialBuildingAI, ICustomAI
    {
        public override void GetConsumptionRates(Randomizer r, int productionRate, out int electricityConsumption, out int waterConsumption, out int sewageAccumulation, out int garbageAccumulation, out int incomeAccumulation)
        {
            electricityConsumption = m_electricityConsumption;
            waterConsumption = m_waterConsumption;
            sewageAccumulation = m_sewageAccumulation;
            garbageAccumulation = m_garbageAccumulation;
            incomeAccumulation = m_incomeAccumulation;

            if (electricityConsumption != 0)
            {
                electricityConsumption = Mathf.Max(100, productionRate * electricityConsumption + r.Int32(100u)) / 100;
            }
            if (waterConsumption != 0)
            {
                int num = r.Int32(100u);
                waterConsumption = Mathf.Max(100, productionRate * waterConsumption + num) / 100;
                if (sewageAccumulation != 0)
                {
                    sewageAccumulation = Mathf.Max(100, productionRate * sewageAccumulation + num) / 100;
                }
            }
            else if (sewageAccumulation != 0)
            {
                sewageAccumulation = Mathf.Max(100, productionRate * sewageAccumulation + r.Int32(100u)) / 100;
            }
            if (garbageAccumulation != 0)
            {
                garbageAccumulation = Mathf.Max(100, productionRate * garbageAccumulation + r.Int32(100u)) / 100;
            }
            if (incomeAccumulation != 0)
            {
                incomeAccumulation = productionRate * incomeAccumulation;
            }
        }

        private void GetConsumption(out int electricityConsumption, out int waterConsumption, out int sewageAccumulation, out int garbageAccumulation, out int incomeAccumulation)
        {
            var r = new Randomizer(GetHashCode());
            electricityConsumption = 0;
            waterConsumption = 0;
            sewageAccumulation = 0;
            garbageAccumulation = 0;
            incomeAccumulation = 0;

            if (UserMod.Settings.UseRPCValues || m_isPloppable)
            {
                var lvl = m_info.m_class.m_level >= 0 ? m_info.m_class.m_level : 0;
                var array = GetArray(m_info, lvl);
                electricityConsumption = array[RPCData.POWER];
                waterConsumption = array[RPCData.WATER];
                sewageAccumulation = array[RPCData.SEWAGE];
                garbageAccumulation = array[RPCData.GARBAGE];
                incomeAccumulation = array[RPCData.INCOME];
                return;
            }

            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService != ItemClass.SubService.CommercialLow)
            {
                if (subService != ItemClass.SubService.CommercialHigh)
                {
                    if (subService != ItemClass.SubService.CommercialLeisure)
                    {
                        if (subService != ItemClass.SubService.CommercialTourist)
                        {
                            if (subService == ItemClass.SubService.CommercialEco)
                            {
                                electricityConsumption = 72;
                                waterConsumption = 70;
                                sewageAccumulation = 70;
                                garbageAccumulation = 40;
                                incomeAccumulation = 700;
                            }
                        }
                        else
                        {
                            electricityConsumption = 30;
                            waterConsumption = 80;
                            sewageAccumulation = 80;
                            garbageAccumulation = 60;
                            incomeAccumulation = 600;
                        }
                    }
                    else
                    {
                        electricityConsumption = 50;
                        waterConsumption = 60;
                        sewageAccumulation = 60;
                        garbageAccumulation = 50;
                        incomeAccumulation = 1000;
                    }
                }
                else
                {
                    if (level != ItemClass.Level.Level1)
                    {
                        if (level != ItemClass.Level.Level2)
                        {
                            if (level == ItemClass.Level.Level3)
                            {
                                electricityConsumption = 30;
                                waterConsumption = 50;
                                sewageAccumulation = 50;
                                garbageAccumulation = 25;
                                incomeAccumulation = 800;
                            }
                        }
                        else
                        {
                            electricityConsumption = 30;
                            waterConsumption = 60;
                            sewageAccumulation = 60;
                            garbageAccumulation = 50;
                            incomeAccumulation = 560;
                        }
                    }
                    else
                    {
                        electricityConsumption = 30;
                        waterConsumption = 70;
                        sewageAccumulation = 70;
                        garbageAccumulation = 50;
                        incomeAccumulation = 280;
                    }
                }
            }
            else
            {
                if (level != ItemClass.Level.Level1)
                {
                    if (level != ItemClass.Level.Level2)
                    {
                        if (level == ItemClass.Level.Level3)
                        {
                            electricityConsumption = 70;
                            waterConsumption = 100;
                            sewageAccumulation = 100;
                            garbageAccumulation = 25;
                            incomeAccumulation = 1120;
                        }
                    }
                    else
                    {
                        electricityConsumption = 60;
                        waterConsumption = 80;
                        sewageAccumulation = 80;
                        garbageAccumulation = 50;
                        incomeAccumulation = 800;
                    }
                }
                else
                {
                    electricityConsumption = 50;
                    waterConsumption = 60;
                    sewageAccumulation = 60;
                    garbageAccumulation = 100;
                    incomeAccumulation = 520;
                }
            }
        }        
    }
}

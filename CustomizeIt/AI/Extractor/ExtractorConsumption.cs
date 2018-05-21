using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Extractor
{
    public partial class CustomizableIndustrialExtractorAI : IndustrialExtractorAI, ICustomAI
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

        public void GetConsumption(out int electricityConsumption, out int waterConsumption, out int sewageAccumulation, out int garbageAccumulation, out int incomeAccumulation)
        {
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

            var r = new Randomizer(GetHashCode());
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService == ItemClass.SubService.IndustrialGeneric)
            {
                if (level != ItemClass.Level.Level1)
                {
                    if (level != ItemClass.Level.Level2)
                    {
                        if (level == ItemClass.Level.Level3)
                        {
                            electricityConsumption = 250;
                            waterConsumption = 160;
                            sewageAccumulation = 160;
                            garbageAccumulation = 100;
                            incomeAccumulation = 240;
                        }
                    }
                    else
                    {
                        electricityConsumption = 200;
                        waterConsumption = 130;
                        sewageAccumulation = 130;
                        garbageAccumulation = 150;
                        incomeAccumulation = 200;
                    }
                }
                else
                {
                    electricityConsumption = 150;
                    waterConsumption = 100;
                    sewageAccumulation = 100;
                    garbageAccumulation = 200;
                    incomeAccumulation = 160;
                }
            }
            else if (subService == ItemClass.SubService.IndustrialOre)
            {
                electricityConsumption = 300;
                waterConsumption = 250;
                sewageAccumulation = 250;
                garbageAccumulation = 150;
                incomeAccumulation = 300;
            }
            else if (subService == ItemClass.SubService.IndustrialOil)
            {
                electricityConsumption = 350;
                waterConsumption = 200;
                sewageAccumulation = 200;
                garbageAccumulation = 200;
                incomeAccumulation = 360;
            }
            else if (subService == ItemClass.SubService.IndustrialForestry)
            {
                electricityConsumption = 90;
                waterConsumption = 60;
                sewageAccumulation = 60;
                garbageAccumulation = 100;
                incomeAccumulation = 140;
            }
            else if (subService == ItemClass.SubService.IndustrialFarming)
            {
                electricityConsumption = 110;
                waterConsumption = 350;
                sewageAccumulation = 350;
                garbageAccumulation = 150;
                incomeAccumulation = 180;
            }
        }
    }
}

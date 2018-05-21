using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Office
{
    public partial class CustomizableOfficeBuildingAI : OfficeBuildingAI, ICustomAI
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
            if (subService == ItemClass.SubService.OfficeGeneric)
            {
                if (level != ItemClass.Level.Level1)
                {
                    if (level != ItemClass.Level.Level2)
                    {
                        if (level == ItemClass.Level.Level3)
                        {
                            electricityConsumption = 120;
                            waterConsumption = 110;
                            sewageAccumulation = 110;
                            garbageAccumulation = 25;
                            incomeAccumulation = 300;
                        }
                    }
                    else
                    {
                        electricityConsumption = 100;
                        waterConsumption = 100;
                        sewageAccumulation = 100;
                        garbageAccumulation = 50;
                        incomeAccumulation = 200;
                    }
                }
                else
                {
                    electricityConsumption = 80;
                    waterConsumption = 90;
                    sewageAccumulation = 90;
                    garbageAccumulation = 100;
                    incomeAccumulation = 140;
                }
            }
            else if (subService == ItemClass.SubService.OfficeHightech)
            {
                electricityConsumption = 130;
                waterConsumption = 100;
                sewageAccumulation = 100;
                garbageAccumulation = 50;
                incomeAccumulation = 260;
            }
        }

    }
}

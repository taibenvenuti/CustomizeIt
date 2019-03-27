using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Office
{
    public partial class CustomizableOfficeBuildingAI : OfficeBuildingAI, ICustomAI
    {
        public override void GetConsumptionRates(ItemClass.Level level, Randomizer r, int productionRate, out int electricityConsumption, out int waterConsumption, out int sewageAccumulation, out int garbageAccumulation, out int incomeAccumulation, out int mailAccumulation) {
            electricityConsumption = m_electricityConsumption;
            waterConsumption = m_waterConsumption;
            sewageAccumulation = m_sewageAccumulation;
            garbageAccumulation = m_garbageAccumulation;
            incomeAccumulation = m_incomeAccumulation;
            mailAccumulation = m_mailAccumulation;
            if (electricityConsumption != 0) {
                electricityConsumption = Mathf.Max(100, productionRate * electricityConsumption + r.Int32(100u)) / 100;
            }
            if (waterConsumption != 0) {
                int num = r.Int32(100u);
                waterConsumption = Mathf.Max(100, productionRate * waterConsumption + num) / 100;
                if (sewageAccumulation != 0) {
                    sewageAccumulation = Mathf.Max(100, productionRate * sewageAccumulation + num) / 100;
                }
            } else if (sewageAccumulation != 0) {
                sewageAccumulation = Mathf.Max(100, productionRate * sewageAccumulation + r.Int32(100u)) / 100;
            }
            if (garbageAccumulation != 0) {
                garbageAccumulation = Mathf.Max(100, productionRate * garbageAccumulation + r.Int32(100u)) / 100;
            }
            if (incomeAccumulation != 0) {
                incomeAccumulation = productionRate * incomeAccumulation;
            }
        }

        public void InitConsumption() {
            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return;
            }

            var r = new Randomizer(m_info.m_prefabDataIndex);
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService == ItemClass.SubService.OfficeGeneric) {
                if (level != ItemClass.Level.Level1) {
                    if (level != ItemClass.Level.Level2) {
                        if (level == ItemClass.Level.Level3) {
                            m_electricityConsumption = 120;
                            m_waterConsumption = 110;
                            m_sewageAccumulation = 110;
                            m_garbageAccumulation = 25;
                            m_incomeAccumulation = 300;
                            m_mailAccumulation = 200;
                        }
                    } else {
                        m_electricityConsumption = 100;
                        m_waterConsumption = 100;
                        m_sewageAccumulation = 100;
                        m_garbageAccumulation = 50;
                        m_incomeAccumulation = 200;
                        m_mailAccumulation = 150;
                    }
                } else {
                    m_electricityConsumption = 80;
                    m_waterConsumption = 90;
                    m_sewageAccumulation = 90;
                    m_garbageAccumulation = 100;
                    m_incomeAccumulation = 140;
                    m_mailAccumulation = 100;
                }
            } else if (subService == ItemClass.SubService.OfficeHightech) {
                m_electricityConsumption = 130;
                m_waterConsumption = 100;
                m_sewageAccumulation = 100;
                m_garbageAccumulation = 50;
                m_incomeAccumulation = 260;
                m_mailAccumulation = 200;
            }
        }
    }
}

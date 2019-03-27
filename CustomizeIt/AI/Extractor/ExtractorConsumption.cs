using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Extractor
{
    public partial class CustomizableIndustrialExtractorAI : IndustrialExtractorAI, ICustomAI
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
            if (mailAccumulation != 0) {
                mailAccumulation = Mathf.Max(100, productionRate * mailAccumulation + r.Int32(100u)) / 100;
            }
        }

        public void InitConsumption() {
            m_electricityConsumption = 0;
            m_waterConsumption = 0;
            m_sewageAccumulation = 0;
            m_garbageAccumulation = 0;
            m_incomeAccumulation = 0;
            m_mailAccumulation = 0;

            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return;
            }

            var r = new Randomizer(m_info.m_prefabDataIndex);
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService == ItemClass.SubService.IndustrialGeneric) {
                if (level != ItemClass.Level.Level1) {
                    if (level != ItemClass.Level.Level2) {
                        if (level == ItemClass.Level.Level3) {
                            m_electricityConsumption = 250;
                            m_waterConsumption = 160;
                            m_sewageAccumulation = 160;
                            m_garbageAccumulation = 100;
                            m_incomeAccumulation = 240;
                            m_mailAccumulation = 100;
                        }
                    } else {
                        m_electricityConsumption = 200;
                        m_waterConsumption = 130;
                        m_sewageAccumulation = 130;
                        m_garbageAccumulation = 150;
                        m_incomeAccumulation = 200;
                        m_mailAccumulation = 75;
                    }
                } else {
                    m_electricityConsumption = 150;
                    m_waterConsumption = 100;
                    m_sewageAccumulation = 100;
                    m_garbageAccumulation = 200;
                    m_incomeAccumulation = 160;
                    m_mailAccumulation = 50;
                }
            } else if (subService == ItemClass.SubService.IndustrialOre) {
                m_electricityConsumption = 300;
                m_waterConsumption = 250;
                m_sewageAccumulation = 250;
                m_garbageAccumulation = 150;
                m_incomeAccumulation = 300;
                m_mailAccumulation = 75;
            } else if (subService == ItemClass.SubService.IndustrialOil) {
                m_electricityConsumption = 350;
                m_waterConsumption = 200;
                m_sewageAccumulation = 200;
                m_garbageAccumulation = 200;
                m_incomeAccumulation = 360;
                m_mailAccumulation = 75;
            } else if (subService == ItemClass.SubService.IndustrialForestry) {
                m_electricityConsumption = 90;
                m_waterConsumption = 60;
                m_sewageAccumulation = 60;
                m_garbageAccumulation = 100;
                m_incomeAccumulation = 140;
                m_mailAccumulation = 75;
            } else if (subService == ItemClass.SubService.IndustrialFarming) {
                m_electricityConsumption = 110;
                m_waterConsumption = 350;
                m_sewageAccumulation = 350;
                m_garbageAccumulation = 150;
                m_incomeAccumulation = 180;
                m_mailAccumulation = 75;
            }
        }
    }
}

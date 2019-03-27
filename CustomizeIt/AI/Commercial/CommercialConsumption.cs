using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Commercial
{
    public partial class CustomizableCommercialBuildingAI : CommercialBuildingAI, ICustomAI
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

        private void InitConsumption() {
            var r = new Randomizer(m_info.m_prefabDataIndex);

            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return;
            }

            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService != ItemClass.SubService.CommercialLow) {
                if (subService != ItemClass.SubService.CommercialHigh) {
                    if (subService != ItemClass.SubService.CommercialLeisure) {
                        if (subService != ItemClass.SubService.CommercialTourist) {
                            if (subService == ItemClass.SubService.CommercialEco) {
                                m_electricityConsumption = 72;
                                m_waterConsumption = 70;
                                m_sewageAccumulation = 70;
                                m_garbageAccumulation = 40;
                                m_incomeAccumulation = 700;
                                m_mailAccumulation = 50;
                            }
                        } else {
                            m_electricityConsumption = 30;
                            m_waterConsumption = 80;
                            m_sewageAccumulation = 80;
                            m_garbageAccumulation = 60;
                            m_incomeAccumulation = 600;
                            m_mailAccumulation = 125;
                        }
                    } else {
                        m_electricityConsumption = 50;
                        m_waterConsumption = 60;
                        m_sewageAccumulation = 60;
                        m_garbageAccumulation = 50;
                        m_incomeAccumulation = 1000;
                        m_mailAccumulation = 75;
                    }
                } else {
                    if (level != ItemClass.Level.Level1) {
                        if (level != ItemClass.Level.Level2) {
                            if (level == ItemClass.Level.Level3) {
                                m_electricityConsumption = 30;
                                m_waterConsumption = 50;
                                m_sewageAccumulation = 50;
                                m_garbageAccumulation = 25;
                                m_incomeAccumulation = 800;
                                m_mailAccumulation = 25;
                            }
                        } else {
                            m_electricityConsumption = 30;
                            m_waterConsumption = 60;
                            m_sewageAccumulation = 60;
                            m_garbageAccumulation = 50;
                            m_incomeAccumulation = 560;
                            m_mailAccumulation = 50;
                        }
                    } else {
                        m_electricityConsumption = 30;
                        m_waterConsumption = 70;
                        m_sewageAccumulation = 70;
                        m_garbageAccumulation = 50;
                        m_incomeAccumulation = 280;
                        m_mailAccumulation = 50;
                    }
                }
            } else {
                if (level != ItemClass.Level.Level1) {
                    if (level != ItemClass.Level.Level2) {
                        if (level == ItemClass.Level.Level3) {
                            m_electricityConsumption = 70;
                            m_waterConsumption = 100;
                            m_sewageAccumulation = 100;
                            m_garbageAccumulation = 25;
                            m_incomeAccumulation = 1120;
                            m_mailAccumulation = 25;
                        }
                    } else {
                        m_electricityConsumption = 60;
                        m_waterConsumption = 80;
                        m_sewageAccumulation = 80;
                        m_garbageAccumulation = 50;
                        m_incomeAccumulation = 800;
                        m_mailAccumulation = 50;
                    }
                } else {
                    m_electricityConsumption = 50;
                    m_waterConsumption = 60;
                    m_sewageAccumulation = 60;
                    m_garbageAccumulation = 100;
                    m_incomeAccumulation = 520;
                    m_mailAccumulation = 100;
                }
            }
        }
    }
}

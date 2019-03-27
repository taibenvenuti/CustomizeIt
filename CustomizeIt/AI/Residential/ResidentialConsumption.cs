using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Residential
{
    public partial class CustomizableResidentialBuildingAI : ResidentialBuildingAI, ICustomAI
    {
        public override void GetConsumptionRates(ItemClass.Level level, Randomizer r, int productionRate, out int electricityConsumption, out int waterConsumption, out int sewageAccumulation, out int garbageAccumulation, out int incomeAccumulation, out int mailAccumulation) {
            electricityConsumption = m_electricityConsumption;
            waterConsumption = m_waterConsumption;
            sewageAccumulation = m_sewageAccumulation;
            garbageAccumulation = m_garbageAccumulation;
            incomeAccumulation = m_incomeAccumulation;
            mailAccumulation = m_mailAccumulation;

            if (electricityConsumption > 0) {
                electricityConsumption = Mathf.Max(100, productionRate * electricityConsumption + r.Int32(100u)) / 100;
            }
            if (waterConsumption > 0) {
                int num = r.Int32(100u);
                waterConsumption = Mathf.Max(100, productionRate * waterConsumption + num) / 100;
                if (sewageAccumulation > 0) {
                    sewageAccumulation = Mathf.Max(100, productionRate * sewageAccumulation + num) / 100;
                }
            } else if (sewageAccumulation > 0) {
                sewageAccumulation = Mathf.Max(100, productionRate * sewageAccumulation + r.Int32(100u)) / 100;
            }
            if (garbageAccumulation > 0) {
                garbageAccumulation = Mathf.Max(100, productionRate * garbageAccumulation + r.Int32(100u)) / 100;
            }
            if (incomeAccumulation > 0) {
                incomeAccumulation = productionRate * incomeAccumulation;
            }
        }

        private void InitConsumption() {

            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return;
            }

            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService != ItemClass.SubService.ResidentialLow) {
                if (subService != ItemClass.SubService.ResidentialHigh) {
                    if (subService != ItemClass.SubService.ResidentialLowEco) {
                        if (subService == ItemClass.SubService.ResidentialHighEco) {
                            switch (level) {
                                case ItemClass.Level.Level1:
                                    m_electricityConsumption = 21;
                                    m_waterConsumption = 60;
                                    m_sewageAccumulation = 60;
                                    m_garbageAccumulation = 49;
                                    m_incomeAccumulation = 49;
                                    m_mailAccumulation = 49;
                                    break;
                                case ItemClass.Level.Level2:
                                    m_electricityConsumption = 20;
                                    m_waterConsumption = 55;
                                    m_sewageAccumulation = 55;
                                    m_garbageAccumulation = 49;
                                    m_incomeAccumulation = 70;
                                    m_mailAccumulation = 49;
                                    break;
                                case ItemClass.Level.Level3:
                                    m_electricityConsumption = 18;
                                    m_waterConsumption = 50;
                                    m_sewageAccumulation = 50;
                                    m_garbageAccumulation = 28;
                                    m_incomeAccumulation = 91;
                                    m_mailAccumulation = 28;
                                    break;
                                case ItemClass.Level.Level4:
                                    m_electricityConsumption = 17;
                                    m_waterConsumption = 45;
                                    m_sewageAccumulation = 45;
                                    m_garbageAccumulation = 21;
                                    m_incomeAccumulation = 112;
                                    m_mailAccumulation = 21;
                                    break;
                                case ItemClass.Level.Level5:
                                    m_electricityConsumption = 15;
                                    m_waterConsumption = 40;
                                    m_sewageAccumulation = 40;
                                    m_garbageAccumulation = 14;
                                    m_incomeAccumulation = 140;
                                    m_mailAccumulation = 14;
                                    break;
                            }
                        }
                    } else {
                        switch (level) {
                            case ItemClass.Level.Level1:
                                m_electricityConsumption = 42;
                                m_waterConsumption = 120;
                                m_sewageAccumulation = 120;
                                m_garbageAccumulation = 70;
                                m_incomeAccumulation = 84;
                                m_mailAccumulation = 70;
                                break;
                            case ItemClass.Level.Level2:
                                m_electricityConsumption = 42;
                                m_waterConsumption = 110;
                                m_sewageAccumulation = 110;
                                m_garbageAccumulation = 70;
                                m_incomeAccumulation = 112;
                                m_mailAccumulation = 70;
                                break;
                            case ItemClass.Level.Level3:
                                m_electricityConsumption = 42;
                                m_waterConsumption = 100;
                                m_sewageAccumulation = 100;
                                m_garbageAccumulation = 42;
                                m_incomeAccumulation = 168;
                                m_mailAccumulation = 42;
                                break;
                            case ItemClass.Level.Level4:
                                m_electricityConsumption = 42;
                                m_waterConsumption = 90;
                                m_sewageAccumulation = 90;
                                m_garbageAccumulation = 28;
                                m_incomeAccumulation = 252;
                                m_mailAccumulation = 28;
                                break;
                            case ItemClass.Level.Level5:
                                m_electricityConsumption = 42;
                                m_waterConsumption = 80;
                                m_sewageAccumulation = 80;
                                m_garbageAccumulation = 21;
                                m_incomeAccumulation = 315;
                                m_mailAccumulation = 21;
                                break;
                        }
                    }
                } else {
                    switch (level) {
                        case ItemClass.Level.Level1:
                            m_electricityConsumption = 30;
                            m_waterConsumption = 60;
                            m_sewageAccumulation = 60;
                            m_garbageAccumulation = 70;
                            m_incomeAccumulation = 70;
                            m_mailAccumulation = 70;
                            break;
                        case ItemClass.Level.Level2:
                            m_electricityConsumption = 28;
                            m_waterConsumption = 55;
                            m_sewageAccumulation = 55;
                            m_garbageAccumulation = 70;
                            m_incomeAccumulation = 100;
                            m_mailAccumulation = 70;
                            break;
                        case ItemClass.Level.Level3:
                            m_electricityConsumption = 26;
                            m_waterConsumption = 50;
                            m_sewageAccumulation = 50;
                            m_garbageAccumulation = 40;
                            m_incomeAccumulation = 130;
                            m_mailAccumulation = 40;
                            break;
                        case ItemClass.Level.Level4:
                            m_electricityConsumption = 24;
                            m_waterConsumption = 45;
                            m_sewageAccumulation = 45;
                            m_garbageAccumulation = 30;
                            m_incomeAccumulation = 160;
                            m_mailAccumulation = 30;
                            break;
                        case ItemClass.Level.Level5:
                            m_electricityConsumption = 22;
                            m_waterConsumption = 40;
                            m_sewageAccumulation = 40;
                            m_garbageAccumulation = 20;
                            m_incomeAccumulation = 200;
                            m_mailAccumulation = 20;
                            break;
                    }
                }
            } else {
                switch (level) {
                    case ItemClass.Level.Level1:
                        m_electricityConsumption = 60;
                        m_waterConsumption = 120;
                        m_sewageAccumulation = 120;
                        m_garbageAccumulation = 100;
                        m_incomeAccumulation = 120;
                        m_mailAccumulation = 100;
                        break;
                    case ItemClass.Level.Level2:
                        m_electricityConsumption = 60;
                        m_waterConsumption = 110;
                        m_sewageAccumulation = 110;
                        m_garbageAccumulation = 100;
                        m_incomeAccumulation = 160;
                        m_mailAccumulation = 100;
                        break;
                    case ItemClass.Level.Level3:
                        m_electricityConsumption = 60;
                        m_waterConsumption = 100;
                        m_sewageAccumulation = 100;
                        m_garbageAccumulation = 60;
                        m_incomeAccumulation = 240;
                        m_mailAccumulation = 60;
                        break;
                    case ItemClass.Level.Level4:
                        m_electricityConsumption = 60;
                        m_waterConsumption = 90;
                        m_sewageAccumulation = 90;
                        m_garbageAccumulation = 40;
                        m_incomeAccumulation = 360;
                        m_mailAccumulation = 40;
                        break;
                    case ItemClass.Level.Level5:
                        m_electricityConsumption = 60;
                        m_waterConsumption = 80;
                        m_sewageAccumulation = 80;
                        m_garbageAccumulation = 30;
                        m_incomeAccumulation = 450;
                        m_mailAccumulation = 30;
                        break;
                }
            }
        }
    }
}

using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Residential
{
    public partial class CustomizableResidentialBuildingAI : ResidentialBuildingAI, ICustomAI
    {
        public override void GetConsumptionRates(Randomizer r, int productionRate, out int electricityConsumption, out int waterConsumption, out int sewageAccumulation, out int garbageAccumulation, out int incomeAccumulation)
        {
            electricityConsumption = m_electricityConsumption;
            waterConsumption = m_waterConsumption;
            sewageAccumulation = m_sewageAccumulation;
            garbageAccumulation = m_garbageAccumulation;
            incomeAccumulation = m_incomeAccumulation;

            if (electricityConsumption > 0)
            {
                electricityConsumption = Mathf.Max(100, productionRate * electricityConsumption + r.Int32(100u)) / 100;
            }
            if (waterConsumption > 0)
            {
                int num = r.Int32(100u);
                waterConsumption = Mathf.Max(100, productionRate * waterConsumption + num) / 100;
                if (sewageAccumulation > 0)
                {
                    sewageAccumulation = Mathf.Max(100, productionRate * sewageAccumulation + num) / 100;
                }
            }
            else if (sewageAccumulation > 0)
            {
                sewageAccumulation = Mathf.Max(100, productionRate * sewageAccumulation + r.Int32(100u)) / 100;
            }
            if (garbageAccumulation > 0)
            {
                garbageAccumulation = Mathf.Max(100, productionRate * garbageAccumulation + r.Int32(100u)) / 100;
            }
            if (incomeAccumulation > 0)
            {
                incomeAccumulation = productionRate * incomeAccumulation;
            }
        }

        private void GetConsumption(out int electricityConsumption, out int waterConsumption, out int sewageAccumulation, out int garbageAccumulation, out int incomeAccumulation)
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

            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService != ItemClass.SubService.ResidentialLow)
            {
                if (subService != ItemClass.SubService.ResidentialHigh)
                {
                    if (subService != ItemClass.SubService.ResidentialLowEco)
                    {
                        if (subService == ItemClass.SubService.ResidentialHighEco)
                        {
                            switch (level)
                            {
                                case ItemClass.Level.Level1:
                                    electricityConsumption = 21;
                                    waterConsumption = 60;
                                    sewageAccumulation = 60;
                                    garbageAccumulation = 49;
                                    incomeAccumulation = 49;
                                    break;
                                case ItemClass.Level.Level2:
                                    electricityConsumption = 20;
                                    waterConsumption = 55;
                                    sewageAccumulation = 55;
                                    garbageAccumulation = 49;
                                    incomeAccumulation = 70;
                                    break;
                                case ItemClass.Level.Level3:
                                    electricityConsumption = 18;
                                    waterConsumption = 50;
                                    sewageAccumulation = 50;
                                    garbageAccumulation = 28;
                                    incomeAccumulation = 91;
                                    break;
                                case ItemClass.Level.Level4:
                                    electricityConsumption = 17;
                                    waterConsumption = 45;
                                    sewageAccumulation = 45;
                                    garbageAccumulation = 21;
                                    incomeAccumulation = 112;
                                    break;
                                case ItemClass.Level.Level5:
                                    electricityConsumption = 15;
                                    waterConsumption = 40;
                                    sewageAccumulation = 40;
                                    garbageAccumulation = 14;
                                    incomeAccumulation = 140;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (level)
                        {
                            case ItemClass.Level.Level1:
                                electricityConsumption = 42;
                                waterConsumption = 120;
                                sewageAccumulation = 120;
                                garbageAccumulation = 70;
                                incomeAccumulation = 84;
                                break;
                            case ItemClass.Level.Level2:
                                electricityConsumption = 42;
                                waterConsumption = 110;
                                sewageAccumulation = 110;
                                garbageAccumulation = 70;
                                incomeAccumulation = 112;
                                break;
                            case ItemClass.Level.Level3:
                                electricityConsumption = 42;
                                waterConsumption = 100;
                                sewageAccumulation = 100;
                                garbageAccumulation = 42;
                                incomeAccumulation = 168;
                                break;
                            case ItemClass.Level.Level4:
                                electricityConsumption = 42;
                                waterConsumption = 90;
                                sewageAccumulation = 90;
                                garbageAccumulation = 28;
                                incomeAccumulation = 252;
                                break;
                            case ItemClass.Level.Level5:
                                electricityConsumption = 42;
                                waterConsumption = 80;
                                sewageAccumulation = 80;
                                garbageAccumulation = 21;
                                incomeAccumulation = 315;
                                break;
                        }
                    }
                }
                else
                {
                    switch (level)
                    {
                        case ItemClass.Level.Level1:
                            electricityConsumption = 30;
                            waterConsumption = 60;
                            sewageAccumulation = 60;
                            garbageAccumulation = 70;
                            incomeAccumulation = 70;
                            break;
                        case ItemClass.Level.Level2:
                            electricityConsumption = 28;
                            waterConsumption = 55;
                            sewageAccumulation = 55;
                            garbageAccumulation = 70;
                            incomeAccumulation = 100;
                            break;
                        case ItemClass.Level.Level3:
                            electricityConsumption = 26;
                            waterConsumption = 50;
                            sewageAccumulation = 50;
                            garbageAccumulation = 40;
                            incomeAccumulation = 130;
                            break;
                        case ItemClass.Level.Level4:
                            electricityConsumption = 24;
                            waterConsumption = 45;
                            sewageAccumulation = 45;
                            garbageAccumulation = 30;
                            incomeAccumulation = 160;
                            break;
                        case ItemClass.Level.Level5:
                            electricityConsumption = 22;
                            waterConsumption = 40;
                            sewageAccumulation = 40;
                            garbageAccumulation = 20;
                            incomeAccumulation = 200;
                            break;
                    }
                }
            }
            else
            {
                switch (level)
                {
                    case ItemClass.Level.Level1:
                        electricityConsumption = 60;
                        waterConsumption = 120;
                        sewageAccumulation = 120;
                        garbageAccumulation = 100;
                        incomeAccumulation = 120;
                        break;
                    case ItemClass.Level.Level2:
                        electricityConsumption = 60;
                        waterConsumption = 110;
                        sewageAccumulation = 110;
                        garbageAccumulation = 100;
                        incomeAccumulation = 160;
                        break;
                    case ItemClass.Level.Level3:
                        electricityConsumption = 60;
                        waterConsumption = 100;
                        sewageAccumulation = 100;
                        garbageAccumulation = 60;
                        incomeAccumulation = 240;
                        break;
                    case ItemClass.Level.Level4:
                        electricityConsumption = 60;
                        waterConsumption = 90;
                        sewageAccumulation = 90;
                        garbageAccumulation = 40;
                        incomeAccumulation = 360;
                        break;
                    case ItemClass.Level.Level5:
                        electricityConsumption = 60;
                        waterConsumption = 80;
                        sewageAccumulation = 80;
                        garbageAccumulation = 30;
                        incomeAccumulation = 450;
                        break;
                }
            }
        }
    }
}

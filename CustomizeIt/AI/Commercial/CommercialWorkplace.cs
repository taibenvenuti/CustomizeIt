using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Commercial
{
    public partial class CustomizableCommercialBuildingAI : CommercialBuildingAI, ICustomAI
    {
        public override void CalculateWorkplaceCount(Randomizer r, int width, int length, out int level0, out int level1, out int level2, out int level3)
        {
            level0 = m_workPlaceCount0;
            level1 = m_workPlaceCount1;
            level2 = m_workPlaceCount2;
            level3 = m_workPlaceCount3;
        }

        private void CalculateWorkplaces(out int level0, out int level1, out int level2, out int level3)
        {
            int num = 0;
            level0 = 100;
            level1 = 0;
            level2 = 0;
            level3 = 0;
            if (UserMod.Settings.UseRPCValues || m_isPloppable)
            {
                var lvl = m_info.m_class.m_level >= 0 ? m_info.m_class.m_level : 0;
                var array = GetArray(m_info, lvl);
                RPCData.CalculateprefabWorkerVisit(m_info, array, out level0, out level1, out level2, out level3, out int visitors);
                return;
            }
            var r = new Randomizer(GetHashCode());
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            var width = m_info.m_cellWidth;
            var length = m_info.m_cellLength;
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
                                num = 100;
                                level0 = 50;
                                level1 = 50;
                                level2 = 0;
                                level3 = 0;
                            }
                        }
                        else
                        {
                            num = 100;
                            level0 = 20;
                            level1 = 20;
                            level2 = 30;
                            level3 = 30;
                        }
                    }
                    else
                    {
                        num = 100;
                        level0 = 30;
                        level1 = 30;
                        level2 = 20;
                        level3 = 20;
                    }
                }
                else if (level == ItemClass.Level.Level1)
                {
                    num = 75;
                    level0 = 0;
                    level1 = 40;
                    level2 = 50;
                    level3 = 10;
                }
                else if (level == ItemClass.Level.Level2)
                {
                    num = 100;
                    level0 = 0;
                    level1 = 20;
                    level2 = 50;
                    level3 = 30;
                }
                else
                {
                    num = 125;
                    level0 = 0;
                    level1 = 0;
                    level2 = 40;
                    level3 = 60;
                }
            }
            else if (level == ItemClass.Level.Level1)
            {
                num = 50;
                level0 = 100;
                level1 = 0;
                level2 = 0;
                level3 = 0;
            }
            else if (level == ItemClass.Level.Level2)
            {
                num = 75;
                level0 = 20;
                level1 = 60;
                level2 = 20;
                level3 = 0;
            }
            else
            {
                num = 100;
                level0 = 5;
                level1 = 15;
                level2 = 30;
                level3 = 50;
            }
            if (num != 0)
            {
                num = Mathf.Max(200, width * length * num + r.Int32(100u)) / 100;
                int num2 = level0 + level1 + level2 + level3;
                if (num2 != 0)
                {
                    level0 = (num * level0 + r.Int32((uint)num2)) / num2;
                    num -= level0;
                }
                num2 = level1 + level2 + level3;
                if (num2 != 0)
                {
                    level1 = (num * level1 + r.Int32((uint)num2)) / num2;
                    num -= level1;
                }
                num2 = level2 + level3;
                if (num2 != 0)
                {
                    level2 = (num * level2 + r.Int32((uint)num2)) / num2;
                    num -= level2;
                }
                level3 = num;
            }
        }
    }
}

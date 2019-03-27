using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Industrial
{
    public partial class CustomizableIndustrialBuildingAI : IndustrialBuildingAI, ICustomAI
    {
        public override void CalculateWorkplaceCount(ItemClass.Level level, Randomizer r, int width, int length, out int level0, out int level1, out int level2, out int level3)
        {
            level0 = m_workPlaceCount0;
            level1 = m_workPlaceCount1;
            level2 = m_workPlaceCount2;
            level3 = m_workPlaceCount3;
        }
        
        public void InitWorkplaces()
        {
            if (UserMod.Settings.UseRPCValues || m_isPloppable)
            {
                //return;
            }
            int num = 0;
            var r = new Randomizer(m_info.m_prefabDataIndex);
            var subService = this.m_info.m_class.m_subService;
            var level = this.m_info.m_class.m_level;
            var width = m_info.m_cellWidth;
            var length = m_info.m_cellLength;
            if (subService == ItemClass.SubService.IndustrialGeneric)
            {
                if (level == ItemClass.Level.Level1)
                {
                    num = 100;
                    m_workPlaceCount0 = 100;
                    m_workPlaceCount1 = 0;
                    m_workPlaceCount2 = 0;
                    m_workPlaceCount3 = 0;
                }
                else if (level == ItemClass.Level.Level2)
                {
                    num = 150;
                    m_workPlaceCount0 = 20;
                    m_workPlaceCount1 = 60;
                    m_workPlaceCount2 = 20;
                    m_workPlaceCount3 = 0;
                }
                else
                {
                    num = 200;
                    m_workPlaceCount0 = 5;
                    m_workPlaceCount1 = 15;
                    m_workPlaceCount2 = 30;
                    m_workPlaceCount3 = 50;
                }
            }
            else if (subService == ItemClass.SubService.IndustrialFarming)
            {
                num = 100;
                m_workPlaceCount0 = 100;
                m_workPlaceCount1 = 0;
                m_workPlaceCount2 = 0;
                m_workPlaceCount3 = 0;
            }
            else if (subService == ItemClass.SubService.IndustrialForestry)
            {
                num = 100;
                m_workPlaceCount0 = 100;
                m_workPlaceCount1 = 0;
                m_workPlaceCount2 = 0;
                m_workPlaceCount3 = 0;
            }
            else if (subService == ItemClass.SubService.IndustrialOre)
            {
                num = 150;
                m_workPlaceCount0 = 20;
                m_workPlaceCount1 = 60;
                m_workPlaceCount2 = 20;
                m_workPlaceCount3 = 0;
            }
            else if (subService == ItemClass.SubService.IndustrialOil)
            {
                num = 150;
                m_workPlaceCount0 = 20;
                m_workPlaceCount1 = 60;
                m_workPlaceCount2 = 20;
                m_workPlaceCount3 = 0;
            }
            if (num != 0)
            {
                num = Mathf.Max(200, width * length * num + r.Int32(100u)) / 100;
                int num2 = m_workPlaceCount0 + m_workPlaceCount1 + m_workPlaceCount2 + m_workPlaceCount3;
                if (num2 != 0)
                {
                    m_workPlaceCount0 = (num * m_workPlaceCount0 + r.Int32((uint)num2)) / num2;
                    num -= m_workPlaceCount0;
                }
                num2 = m_workPlaceCount1 + m_workPlaceCount2 + m_workPlaceCount3;
                if (num2 != 0)
                {
                    m_workPlaceCount1 = (num * m_workPlaceCount1 + r.Int32((uint)num2)) / num2;
                    num -= m_workPlaceCount1;
                }
                num2 = m_workPlaceCount2 + m_workPlaceCount3;
                if (num2 != 0)
                {
                    m_workPlaceCount2 = (num * m_workPlaceCount2 + r.Int32((uint)num2)) / num2;
                    num -= m_workPlaceCount2;
                }
                m_workPlaceCount3 = num;
            }
        }
    }
}

using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Office
{
    public partial class CustomizableOfficeBuildingAI : OfficeBuildingAI, ICustomAI
    {
        public override void CalculateWorkplaceCount(ItemClass.Level level, Randomizer r, int width, int length, out int level0, out int level1, out int level2, out int level3) {
            level0 = m_workPlaceCount0;
            level1 = m_workPlaceCount1;
            level2 = m_workPlaceCount2;
            level3 = m_workPlaceCount3;
        }

        public void InitWorkplaces() {

            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return;
            }
            int num = 0;
            var r = new Randomizer(m_info.m_prefabDataIndex);
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            var width = m_info.m_cellWidth;
            var length = m_info.m_cellLength;
            if (subService == ItemClass.SubService.OfficeGeneric) {
                if (level == ItemClass.Level.Level1) {
                    num = 50;
                    m_workPlaceCount0 = 0;
                    m_workPlaceCount1 = 40;
                    m_workPlaceCount2 = 50;
                    m_workPlaceCount3 = 10;
                } else if (level == ItemClass.Level.Level2) {
                    num = 110;
                    m_workPlaceCount0 = 0;
                    m_workPlaceCount1 = 20;
                    m_workPlaceCount2 = 50;
                    m_workPlaceCount3 = 30;
                } else {
                    num = 170;
                    m_workPlaceCount0 = 0;
                    m_workPlaceCount1 = 0;
                    m_workPlaceCount2 = 40;
                    m_workPlaceCount3 = 60;
                }
            } else if (subService == ItemClass.SubService.OfficeHightech) {
                num = 55;
                m_workPlaceCount0 = 0;
                m_workPlaceCount1 = 10;
                m_workPlaceCount2 = 40;
                m_workPlaceCount3 = 50;
            }
            if (num != 0) {
                num = Mathf.Max(200, width * length * num + r.Int32(100u)) / 100;
                int num2 = m_workPlaceCount0 + m_workPlaceCount1 + m_workPlaceCount2 + m_workPlaceCount3;
                if (num2 != 0) {
                    m_workPlaceCount0 = (num * m_workPlaceCount0 + r.Int32((uint)num2)) / num2;
                    num -= m_workPlaceCount0;
                }
                num2 = m_workPlaceCount1 + m_workPlaceCount2 + m_workPlaceCount3;
                if (num2 != 0) {
                    m_workPlaceCount1 = (num * m_workPlaceCount1 + r.Int32((uint)num2)) / num2;
                    num -= m_workPlaceCount1;
                }
                num2 = m_workPlaceCount2 + m_workPlaceCount3;
                if (num2 != 0) {
                    m_workPlaceCount2 = (num * m_workPlaceCount2 + r.Int32((uint)num2)) / num2;
                    num -= m_workPlaceCount2;
                }
                m_workPlaceCount3 = num;
            }
        }
    }
}

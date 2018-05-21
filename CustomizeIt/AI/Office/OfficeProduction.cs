using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Office
{
    public partial class CustomizableOfficeBuildingAI : OfficeBuildingAI, ICustomAI
    {
        public override int CalculateProductionCapacity(Randomizer r, int width, int length)
        {
            return m_productionCapacity;
        }

        public int CalculateProduction()
        {

            if (UserMod.Settings.UseRPCValues || m_isPloppable)
            {
                var lvl = m_info.m_class.m_level >= 0 ? m_info.m_class.m_level : 0;
                var array = GetArray(m_info, lvl);
                var workers = m_workPlaceCount0 + m_workPlaceCount1 + m_workPlaceCount2 + m_workPlaceCount3;
                return Mathf.Max(1, workers / array[RPCData.PRODUCTION]);
            }

            var r = new Randomizer(GetHashCode());
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            var width = m_info.m_cellWidth;
            var length = m_info.m_cellLength;
            int num = 0;
            if (subService == ItemClass.SubService.OfficeGeneric)
            {
                if (level == ItemClass.Level.Level1)
                {
                    num = 50;
                }
                else if (level == ItemClass.Level.Level2)
                {
                    num = 70;
                }
                else
                {
                    num = 80;
                }
            }
            else if (subService == ItemClass.SubService.OfficeHightech)
            {
                num = 100;
            }
            if (num != 0)
            {
                num = Mathf.Max(100, width * length * num + r.Int32(100u)) / 100;
            }
            return num;
        }

    }
}

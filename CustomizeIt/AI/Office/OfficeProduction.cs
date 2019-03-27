using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Office
{
    public partial class CustomizableOfficeBuildingAI : OfficeBuildingAI, ICustomAI
    {
        public override int CalculateProductionCapacity(ItemClass.Level level, Randomizer r, int width, int length) {
            return m_productionCapacity;
        }

        public void InitProduction() {
            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return Mathf.Max(1, workers / array[RPCData.PRODUCTION]);
            }

            var r = new Randomizer(m_info.m_prefabDataIndex);
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            var width = m_info.m_cellWidth;
            var length = m_info.m_cellLength;
            int num = 0;
            if (subService == ItemClass.SubService.OfficeGeneric) {
                if (level == ItemClass.Level.Level1) {
                    num = 50;
                } else if (level == ItemClass.Level.Level2) {
                    num = 70;
                } else {
                    num = 80;
                }
            } else if (subService == ItemClass.SubService.OfficeHightech) {
                num = 100;
            }
            if (num != 0) {
                num = Mathf.Max(100, width * length * num + r.Int32(100u)) / 100;
            }
            m_productionCapacity = num;
        }
    }
}

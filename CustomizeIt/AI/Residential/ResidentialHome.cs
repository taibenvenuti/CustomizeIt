using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Residential
{
    public partial class CustomizableResidentialBuildingAI : ResidentialBuildingAI, ICustomAI
    {
        public override int CalculateHomeCount(ItemClass.Level level, Randomizer r, int width, int length) {
            return m_homeCount;
        }

        private void InitHomes() {
            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return RPCData.CalculatePrefabHousehold(m_info, array);
            }
            var r = new Randomizer(SimulationManager.instance.m_currentGameTime.Ticks);
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            var width = m_info.m_cellWidth;
            var length = m_info.m_cellLength;
            int num;
            if (subService == ItemClass.SubService.ResidentialLow || subService == ItemClass.SubService.ResidentialLowEco) {
                if (level == ItemClass.Level.Level1) {
                    num = 20;
                } else if (level == ItemClass.Level.Level2) {
                    num = 25;
                } else if (level == ItemClass.Level.Level3) {
                    num = 30;
                } else if (level == ItemClass.Level.Level4) {
                    num = 35;
                } else {
                    num = 40;
                }
            } else if (level == ItemClass.Level.Level1) {
                num = 60;
            } else if (level == ItemClass.Level.Level2) {
                num = 100;
            } else if (level == ItemClass.Level.Level3) {
                num = 130;
            } else if (level == ItemClass.Level.Level4) {
                num = 150;
            } else {
                num = 160;
            }
            m_homeCount = Mathf.Max(100, width * length * num + r.Int32(100u)) / 100;
        }
    }
}

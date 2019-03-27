using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Industrial
{
    public partial class CustomizableIndustrialBuildingAI : IndustrialBuildingAI, ICustomAI
    {
        public override int CalculateProductionCapacity(ItemClass.Level level, Randomizer r, int width, int length)
        {
            return m_productionCapacity;
        }

        public void InitProduction()
        {
            var r = new Randomizer(m_info.m_prefabDataIndex);
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            var width = m_info.m_cellWidth;
            var length = m_info.m_cellLength;
            int num;
            if (UserMod.Settings.UseRPCValues || m_isPloppable)
            {
                //return Mathf.Max(100, width * length * array[RPCData.PRODUCTION]) / 100;
            }
            if (subService == ItemClass.SubService.IndustrialGeneric)
            {
                if (level == ItemClass.Level.Level1)
                {
                    num = 100;
                }
                else if (level == ItemClass.Level.Level2)
                {
                    num = 140;
                }
                else
                {
                    num = 160;
                }
            }
            else
            {
                num = 100;
            }
            if (num != 0)
            {
                num = Mathf.Max(100, width * length * num + r.Int32(100u)) / 100;
            }
            m_productionCapacity = num;
        }
    }
}

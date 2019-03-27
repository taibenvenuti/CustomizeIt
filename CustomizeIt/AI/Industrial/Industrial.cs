using ColossalFramework.Math;
using System.Text;

namespace CustomizeIt.AI.Industrial
{
    public partial class CustomizableIndustrialBuildingAI : IndustrialBuildingAI, ICustomAI
    {
        public bool m_isPloppable;
        public int m_workPlaceCount0;
        public int m_workPlaceCount1;
        public int m_workPlaceCount2;
        public int m_workPlaceCount3;
        public int m_electricityConsumption;
        public int m_waterConsumption;
        public int m_sewageAccumulation;
        public int m_garbageAccumulation;
        public int m_incomeAccumulation;
        public int m_productionCapacity;
        public int m_noiseAccumulation;
        public int m_pollutionAccumulation;
        public int m_mailAccumulation;

        public void Initialize(bool isPloppable = false) {
            m_isPloppable = isPloppable;
            AssignClass();
            InitWorkplaces();
            InitConsumption();
            InitPollution();
            InitProduction();
        }

        private void AssignClass() {
            if (!m_info.m_mesh.name.ToLower().Contains("customizable")) return;
            var itemClass = new StringBuilder();
            if (m_info.m_mesh.name.ToLower().Contains("ore")) itemClass.Append("Ore");
            else if (m_info.m_mesh.name.ToLower().Contains("oil")) itemClass.Append("Oil");
            else if (m_info.m_mesh.name.ToLower().Contains("forest")) itemClass.Append("Forestry");
            else if (m_info.m_mesh.name.ToLower().Contains("farm")) itemClass.Append("Farming");
            else itemClass.Append("Industrial");
            itemClass.Append(" - ");
            if (!m_info.m_mesh.name.ToLower().Contains("ore") && !m_info.m_mesh.name.ToLower().Contains("oil") && !m_info.m_mesh.name.ToLower().Contains("forest") && !m_info.m_mesh.name.ToLower().Contains("farm")) {
                itemClass.Append("Level");
                if (m_info.m_mesh.name.ToLower().Contains("5") || m_info.m_mesh.name.ToLower().Contains("4") || m_info.m_mesh.name.ToLower().Contains("3")) itemClass.Append("3");
                else if (m_info.m_mesh.name.ToLower().Contains("2")) itemClass.Append("2");
                else itemClass.Append("1");
            } else itemClass.Append("Processing");
            m_info.m_class = ItemClassCollection.FindClass(itemClass.ToString());
            m_info.m_placementStyle = ItemClass.Placement.Manual;
            m_info.m_autoRemove = true;
        }

        public override void BuildingUpgraded(ushort buildingID, ref Building data) {
            CalculateWorkplaceCount((ItemClass.Level)data.m_level, new Randomizer(buildingID), data.Width, data.Length, out int num, out int num2, out int num3, out int num4);
            int workCount = num + num2 + num3 + num4;
            int homeCount = 0;
            int visitCount = 0;
            SharedAI.EnsureCitizenUnits(buildingID, m_info, ref data, homeCount, workCount, visitCount, 0);
        }

        public override void BuildingLoaded(ushort buildingID, ref Building data, uint version) {
            CalculateWorkplaceCount((ItemClass.Level)data.m_level, new Randomizer(buildingID), data.Width, data.Length, out int num, out int num2, out int num3, out int num4);
            int workCount = num + num2 + num3 + num4;
            int homeCount = 0;
            int visitCount = 0;
            SharedAI.EnsureCitizenUnits(buildingID, m_info, ref data, homeCount, workCount, visitCount, 0);
        }
    }
}

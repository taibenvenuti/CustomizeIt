using ColossalFramework.Math;
using System.Text;

namespace CustomizeIt.AI.Residential
{
    public partial class CustomizableResidentialBuildingAI : ResidentialBuildingAI, ICustomAI
    {
        public bool m_isPloppable;
        public int m_homeCount;
        public int m_electricityConsumption;
        public int m_waterConsumption;
        public int m_sewageAccumulation;
        public int m_garbageAccumulation;
        public int m_incomeAccumulation;
        public int m_mailAccumulation;

        public void Initialize(bool isPloppable = false) {
            m_isPloppable = isPloppable;
            AssignClass();
            InitHomes();
            InitConsumption();
        }

        private void AssignClass() {
            if (!m_info.m_mesh.name.ToLower().Contains("customizable")) return;
            var itemClass = new StringBuilder();
            if (m_info.m_mesh.name.ToLower().Contains("high")) itemClass.Append("High");
            else itemClass.Append("Low");
            itemClass.Append(" Residential");
            if (m_info.m_mesh.name.ToLower().Contains("eco") && SteamHelper.IsDLCOwned(SteamHelper.DLC.GreenCitiesDLC))
                itemClass.Append(" Eco");
            itemClass.Append(" - Level");
            if (m_info.m_mesh.name.ToLower().Contains("5")) itemClass.Append("5");
            else if (m_info.m_mesh.name.ToLower().Contains("4")) itemClass.Append("4");
            else if (m_info.m_mesh.name.ToLower().Contains("3")) itemClass.Append("3");
            else if (m_info.m_mesh.name.ToLower().Contains("2")) itemClass.Append("2");
            else itemClass.Append("1");
            m_info.m_class = ItemClassCollection.FindClass(itemClass.ToString());
            m_info.m_placementStyle = ItemClass.Placement.Manual;
            m_info.m_autoRemove = true;
        }

        public override void BuildingUpgraded(ushort buildingID, ref Building data) {
            int workCount = 0;
            int homeCount = CalculateHomeCount((ItemClass.Level)data.m_level, new Randomizer(buildingID), data.Width, data.Length);
            int visitCount = 0;
            SharedAI.EnsureCitizenUnits(buildingID, m_info, ref data, homeCount, workCount, visitCount, 0);
        }

        public override void BuildingLoaded(ushort buildingID, ref Building data, uint version) {
            int workCount = 0;
            int homeCount = CalculateHomeCount((ItemClass.Level)data.m_level, new Randomizer(buildingID), data.Width, data.Length);
            int visitCount = 0;
            SharedAI.EnsureCitizenUnits(buildingID, m_info, ref data, homeCount, workCount, visitCount, 0);
        }
    }
}

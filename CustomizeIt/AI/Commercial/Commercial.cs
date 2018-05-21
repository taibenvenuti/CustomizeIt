using ColossalFramework.Math;
using System.Text;

namespace CustomizeIt.AI.Commercial
{
    public partial class CustomizableCommercialBuildingAI : CommercialBuildingAI, ICustomAI
    {
        public bool m_isPloppable;
        public int m_visitors;
        public int m_workPlaceCount0;
        public int m_workPlaceCount1;
        public int m_workPlaceCount2;
        public int m_workPlaceCount3;
        public int m_electricityConsumption;
        public int m_waterConsumption;
        public int m_sewageAccumulation;
        public int m_garbageAccumulation;
        public int m_incomeAccumulation;
        public int m_noiseAccumulation;

        public void Initialize(bool isPloppable = false)
        {
            m_isPloppable = isPloppable;
            AssignClass();
            CalculateWorkplaces(out m_workPlaceCount0, out m_workPlaceCount1, out m_workPlaceCount2, out m_workPlaceCount3);
            m_visitors = CalculateVisitplaces();
            GetConsumption(out m_electricityConsumption, out m_waterConsumption, out m_sewageAccumulation, out m_garbageAccumulation, out m_incomeAccumulation);
            GetPollution(out m_noiseAccumulation);
        }

        private void AssignClass()
        {
            if (!m_info.m_mesh.name.ToLower().Contains("customizable")) return;
            var itemClass = new StringBuilder();
            if (m_info.m_mesh.name.ToLower().Contains("high")) itemClass.Append("High");
            else if (m_info.m_mesh.name.ToLower().Contains("eco") && SteamHelper.IsDLCOwned(SteamHelper.DLC.GreenCitiesDLC)) itemClass.Append("Eco");
            else if (m_info.m_mesh.name.ToLower().Contains("leisure") && SteamHelper.IsDLCOwned(SteamHelper.DLC.AfterDarkDLC)) itemClass.Append("Leisure");
            else if (m_info.m_mesh.name.ToLower().Contains("tourist") && SteamHelper.IsDLCOwned(SteamHelper.DLC.AfterDarkDLC)) itemClass.Append("Tourist");
            else itemClass.Append("Low");
            itemClass.Append(" Commercial");
            if (!m_info.m_mesh.name.ToLower().Contains("eco") && !m_info.m_mesh.name.ToLower().Contains("leisure") && !m_info.m_mesh.name.ToLower().Contains("tourist"))
            {
                itemClass.Append(" - Level");
                if (m_info.m_mesh.name.ToLower().Contains("5") || m_info.m_mesh.name.ToLower().Contains("4") || m_info.m_mesh.name.ToLower().Contains("3")) itemClass.Append("3");
                else if (m_info.m_mesh.name.ToLower().Contains("2")) itemClass.Append("2");
                else itemClass.Append("1");
            }
            else if(m_info.m_mesh.name.ToLower().Contains("tourist") && SteamHelper.IsDLCOwned(SteamHelper.DLC.AfterDarkDLC))
            {
                if(m_info.m_mesh.name.ToLower().Contains("beach")) itemClass.Append(" - Beach");
                else itemClass.Append(" - Land");
            }
            m_info.m_class = ItemClassCollection.FindClass(itemClass.ToString());
            m_info.m_placementStyle = ItemClass.Placement.Manual;
            m_info.m_autoRemove = true;
        }

        public override void BuildingUpgraded(ushort buildingID, ref Building data)
        {
            CalculateWorkplaceCount(new Randomizer(buildingID), data.Width, data.Length, out int num, out int num2, out int num3, out int num4);
            int workCount = num + num2 + num3 + num4;
            int homeCount = 0;
            int visitCount = CalculateVisitplaceCount(new Randomizer(buildingID), data.Width, data.Length);
            SharedAI.EnsureCitizenUnits(buildingID, m_info, ref data, homeCount, workCount, visitCount, 0);
        }

        public override void BuildingLoaded(ushort buildingID, ref Building data, uint version)
        {
            CalculateWorkplaceCount(new Randomizer(buildingID), data.Width, data.Length, out int num, out int num2, out int num3, out int num4);
            int workCount = num + num2 + num3 + num4;
            int homeCount = 0;
            int visitCount = CalculateVisitplaceCount(new Randomizer(buildingID), data.Width, data.Length);
            SharedAI.EnsureCitizenUnits(buildingID, m_info, ref data, homeCount, workCount, visitCount, 0);
        }

        private int[] GetArray(BuildingInfo item, ItemClass.Level level)
        {
            int[][] array;

            try
            {
                switch (item.m_class.m_subService)
                {
                    case ItemClass.SubService.CommercialLeisure:
                        array = RPCData.commercialLeisure;
                        break;

                    case ItemClass.SubService.CommercialTourist:
                        array = RPCData.commercialTourist;
                        break;

                    case ItemClass.SubService.CommercialEco:
                        array = RPCData.commercialEcoLow;
                        break;

                    case ItemClass.SubService.CommercialHigh:
                        array = RPCData.commercialHigh;
                        break;

                    case ItemClass.SubService.CommercialLow:
                    default:
                        array = RPCData.commercialLow;
                        break;
                }

                return array[(int)level];
            }
            catch (System.Exception)
            {
                return RPCData.commercialLow[0];
            }
        }
    }
}

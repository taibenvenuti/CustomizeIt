using ColossalFramework.UI;
using CustomizeIt.TranslationFramework;
using ICities;
using UnityEngine;

namespace CustomizeIt
{
    public class UserMod : IUserMod
    {
        public static Translation Translation = new Translation();
        public static string name = "Customize It!";
        public string Name => name;
        public string Description
        {
            get
            {
                if (!Loading.IsHooked())
                {
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                        "Missing dependency",
                        $"{name} requires the 'Prefab Hook' mod to work properly. Please subscribe to the mod and restart the game!",
                        false);
                }
                return Translation.GetTranslation("CUSTOMIZE-IT-MODDESCRIPTION");
            }
        }
        internal static CustomizeItSettings settings;
        public static CustomizeItSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = CustomizeItSettings.Load();
                    if (settings == null)
                    {
                        settings = new CustomizeItSettings();
                        settings.Save();
                    }
                }
                return settings;
            }
            set
            {
                settings = value;
            }
        }
        private CustomizeIt Instance => CustomizeIt.instance;

        public void OnSettingsUI(UIHelperBase helper)
        {
            helper.AddSpace(10);
            Instance.SavePerCityCheckBox = (UICheckBox)helper.AddCheckbox(Instance.CheckboxText, Settings.SavePerCity, (b) => 
            {
                Settings.SavePerCity = b;
                Settings.Save();
            });
            Instance.SavePerCityCheckBox.parent.Find<UILabel>("Label").disabledTextColor = Color.gray;
            helper.AddSpace(10);
            Instance.ResetAllButton = (UIButton)helper.AddButton(Instance.ButtonText, () => 
            {
                SimulationManager.instance.AddAction(() =>
                {
                    for (uint i = 0; i < PrefabCollection<BuildingInfo>.LoadedCount(); i++)
                    {
                        var building = PrefabCollection<BuildingInfo>.GetLoaded(i);
                        if (building == null || building.m_buildingAI == null || !(building.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI)))) continue;
                        CustomizeIt.instance.ResetBuilding(building);
                    }
                });
            });
            Instance.ToggleOptionPanelControls(false);
        }
    }
}

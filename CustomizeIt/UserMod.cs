﻿using ColossalFramework.UI;
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
        public string Description => Translation.GetTranslation("CUSTOMIZE-IT-MODDESCRIPTION");
        internal static CustomizeItSettings settings;
        public static CustomizeItSettings Settings {
            get {
                if (settings == null) {
                    settings = CustomizeItSettings.Load();
                    if (settings == null) {
                        settings = new CustomizeItSettings();
                        settings.Save();
                    }
                }
                return settings;
            }
            set {
                settings = value;
            }
        }
        private CustomizeIt Instance => CustomizeIt.instance;

        public void OnSettingsUI(UIHelperBase helper) {
            helper.AddSpace(10);
            Instance.SavePerCityCheckBox = (UICheckBox)helper.AddCheckbox(Instance.SavePerCityText, Settings.SavePerCity, (b) => {
                Settings.SavePerCity = b;
                Settings.Save();
            });
            Instance.SavePerCityCheckBox.parent.Find<UILabel>("Label").disabledTextColor = Color.gray;
            helper.AddSpace(10);
            Instance.UseRPCValuesCheckBox = (UICheckBox)helper.AddCheckbox(Instance.UseRPCValuesText, Settings.UseRPCValues, (b) => {
                Settings.UseRPCValues = b;
                Settings.Save();
            });
            helper.AddSpace(10);
            Instance.ResetAllButton = (UIButton)helper.AddButton(Instance.ButtonText, () => {
                SimulationManager.instance.AddAction(() => {
                    for (uint i = 0; i < PrefabCollection<BuildingInfo>.PrefabCount(); i++) {
                        var building = PrefabCollection<BuildingInfo>.GetPrefab(i);
                        if (building == null || building.m_buildingAI == null || (!building.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI)) && !building.m_buildingAI.GetType().IsSubclassOf(typeof(PrivateBuildingAI)))) continue;
                        CustomizeIt.instance.ResetBuilding(building);
                    }
                });
            });
            Instance.ToggleOptionPanelControls(false);
        }
    }
}

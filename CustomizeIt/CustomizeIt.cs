using ColossalFramework;
using ColossalFramework.UI;
using CustomizeIt.GUI;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CustomizeIt
{
    public class CustomizeIt : Singleton<CustomizeIt>
    {
        internal Dictionary<string, CustomizableProperties> CustomBuildingData = new Dictionary<string, CustomizableProperties>();
        internal Dictionary<string, CustomizableProperties> OriginalBuildingData = new Dictionary<string, CustomizableProperties>();
        private List<string> ricoBuildings;
        internal List<string> RICOBuildings {
            get {
                if (ricoBuildings == null) {
                    ricoBuildings = new List<string>();
                }
                return ricoBuildings;
            }
        }
        private bool initialized;
        private bool initializedButtons;
        internal BuildingInfo CurrentBuilding;
        internal FootballPanel FootballPanel;
        internal CityServiceWorldInfoPanel CityServiceWorldInfoPanel;
        internal ZonedBuildingWorldInfoPanel ZonedBuildingWorldInfoPanel;
        internal ShelterWorldInfoPanel ShelterWorldInfoPanel;
        private UIButton serviceButton;
        private UIButton zonedButton;
        private UIButton shelterButton;
        private UIButton footballButton;
        internal UIPanelWrapper CustomizePanel;
        internal UICheckBox SavePerCityCheckBox;
        internal UIButton ResetAllButton;
        internal UICheckBox UseRPCValuesCheckBox;
        internal string SavePerCityText => UserMod.Translation.GetTranslation("CUSTOMIZE-IT-SAVE-PER-CITY");
        internal string ButtonText => UserMod.Translation.GetTranslation("CUSTOMIZE-IT-RESET-ALL");
        internal string ButtonTooltip => ResetAllButton != null && ResetAllButton.isEnabled ? null : UserMod.Translation.GetTranslation("CUSTOMIZE-IT-OPTION-INGAME-TOOLTIP");
        internal string CheckBoxTooltip => SavePerCityCheckBox != null && SavePerCityCheckBox.isEnabled ? null : UserMod.Translation.GetTranslation("CUSTOMIZE-IT-OPTION-MAINMENU-TOOLTIP");
        internal string UseRPCValuesText => UserMod.Translation.GetTranslation("CUSTOMIZE-IT-USE-RPC-VALUES");
        internal string ConvertRICOText => UserMod.Translation.GetTranslation("CUSTOMIZE-IT-CONVERT-RICO");

        internal void Initialize() {
            if (initialized) return;
            ToggleOptionPanelControls(true);
            AddPanelButtons();
            initialized = true;
        }

        internal void Release() {
            initialized = false;
            initializedButtons = false;
        }

        internal void Customize(BuildingInfo building) {
            if (building == null || building.m_buildingAI == null || (!building.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI)) && !building.m_buildingAI.GetType().IsSubclassOf(typeof(PrivateBuildingAI)))) return;
            if (!OriginalBuildingData.TryGetValue(building.name, out CustomizableProperties originalProperties))
                OriginalBuildingData.Add(building.name, building.GetCustomizableProperties());
            else OriginalBuildingData[building.name] = building.GetCustomizableProperties();
            if (CustomBuildingData.TryGetValue(building.name, out CustomizableProperties customProperties))
                building.LoadCustomProperties(customProperties);
        }

        public void SaveBuilding(BuildingInfo building) {
            if (!CustomBuildingData.TryGetValue(building.name, out CustomizableProperties customProperties))
                CustomBuildingData.Add(building.name, new CustomizableProperties(building));
            else CustomBuildingData[building.name] = new CustomizableProperties(building);

            if (!UserMod.Settings.SavePerCity) UserMod.Settings.Save();
        }

        public void ResetBuilding(BuildingInfo building) {
            var properties = building.ResetProperties();
            if (CustomBuildingData.TryGetValue(building.name, out CustomizableProperties customProperties))
                CustomBuildingData.Remove(building.name);
            if (properties != null) building.LoadCustomProperties(properties);
            if (!UserMod.Settings.SavePerCity) UserMod.Settings.Save();
        }

        private void AddPanelButtons() {
            if (!initializedButtons) {
                CityServiceWorldInfoPanel = GameObject.Find("(Library) CityServiceWorldInfoPanel").GetComponent<CityServiceWorldInfoPanel>();
                if (CityServiceWorldInfoPanel != null) {
                    AddBuildingPanelControls(CityServiceWorldInfoPanel, out serviceButton, new Vector3(-7f, 43f, 0f));
                    serviceButton.name = "ServiceButton";
                }
                ZonedBuildingWorldInfoPanel = GameObject.Find("(Library) ZonedBuildingWorldInfoPanel").GetComponent<ZonedBuildingWorldInfoPanel>();
                if (ZonedBuildingWorldInfoPanel != null) {
                    AddBuildingPanelControls(ZonedBuildingWorldInfoPanel, out zonedButton, new Vector3(-7f, 43f, 0f));
                    zonedButton.name = "ZonedButton";
                }
                ShelterWorldInfoPanel = GameObject.Find("(Library) ShelterWorldInfoPanel").GetComponent<ShelterWorldInfoPanel>();
                if (ShelterWorldInfoPanel != null) {
                    AddBuildingPanelControls(ShelterWorldInfoPanel, out shelterButton, new Vector3(-7f, 43f, 0f));
                    shelterButton.name = "ShelterButton";
                }
                FootballPanel = GameObject.Find("(Library) FootballPanel").GetComponent<FootballPanel>();
                if (FootballPanel != null) {
                    AddBuildingPanelControls(FootballPanel, out footballButton, new Vector3(-7f, 43f, 0f));
                    footballButton.name = "IndustryButton";
                }
                initializedButtons = true;
            }
        }

        private void AddBuildingPanelControls(WorldInfoPanel infoPanel, out UIButton button, Vector3 customizeButtonOffset) {
            button = UIUtil.CreateToggleButton(infoPanel.component, customizeButtonOffset, UIAlignAnchor.TopRight, delegate (UIComponent component, UIMouseEventParameter param) {
                InstanceID instanceID = CityServiceWorldInfoPanel.component.isVisible ?
                (InstanceID)CityServiceWorldInfoPanel.GetType().GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(CityServiceWorldInfoPanel) :
                ZonedBuildingWorldInfoPanel.component.isVisible ?
                (InstanceID)ZonedBuildingWorldInfoPanel.GetType().GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ZonedBuildingWorldInfoPanel) :
                (InstanceID)ShelterWorldInfoPanel.GetType().GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(ShelterWorldInfoPanel);
                var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;
                if ((CustomizePanel == null || building != CurrentBuilding) && building.m_buildingAI.GetType() != typeof(DummyBuildingAI))
                    CustomizePanel = building.GenerateCustomizationPanel();
                else {
                    CustomizePanel.isVisible = false;
                    UIUtil.DestroyDeeply(CustomizePanel);
                }
                if (component.hasFocus) component.Unfocus();
            });
        }

        internal void ToggleOptionPanelControls(bool inGame) {
            SavePerCityCheckBox.isEnabled = !inGame;
            UseRPCValuesCheckBox.isEnabled = !inGame;
            ResetAllButton.isEnabled = inGame;
            ResetAllButton.tooltip = ButtonTooltip;
            SavePerCityCheckBox.tooltip = CheckBoxTooltip;
            UseRPCValuesCheckBox.tooltip = CheckBoxTooltip;
            SavePerCityCheckBox.Find<UISprite>("Unchecked").spriteName = inGame ? "ToggleBaseDisabled" : "ToggleBase";
            ((UISprite)UseRPCValuesCheckBox.checkedBoxObject).spriteName = inGame ? "ToggleBaseDisabled" : "ToggleBaseFocused";
            UseRPCValuesCheckBox.Find<UISprite>("Unchecked").spriteName = inGame ? "ToggleBaseDisabled" : "ToggleBase";
            ((UISprite)UseRPCValuesCheckBox.checkedBoxObject).spriteName = inGame ? "ToggleBaseDisabled" : "ToggleBaseFocused";
        }
    }
}

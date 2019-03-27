using ColossalFramework;
using ColossalFramework.UI;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using CustomizeIt.GUI;

namespace CustomizeIt
{
    public class CustomizeIt : Singleton<CustomizeIt>
    {
        internal Dictionary<string, CustomizableProperties> CustomBuildingData = new Dictionary<string, CustomizableProperties>();
        internal Dictionary<string, CustomizableProperties> OriginalBuildingData = new Dictionary<string, CustomizableProperties>();
        private bool initialized;
        private bool inizializedButton;
        internal BuildingInfo CurrentBuilding;
        internal CityServiceWorldInfoPanel ServiceBuildingInfoPanel;
        private UIButton customizeButton;
        internal UIPanelWrapper CustomizePanel;
        internal UICheckBox SavePerCityCheckBox;
        internal UIButton ResetAllButton;
        internal string CheckboxText => UserMod.Translation.GetTranslation("CUSTOMIZE-IT-CHECKBOX");
        internal string ButtonText => UserMod.Translation.GetTranslation("CUSTOMIZE-IT-RESET-ALL");
        internal string ButtonTooltip => ResetAllButton != null && ResetAllButton.isEnabled ? null : UserMod.Translation.GetTranslation("CUSTOMIZE-IT-OPTION-INGAME-TOOLTIP");
        internal string CheckBoxTooltip => SavePerCityCheckBox != null && SavePerCityCheckBox.isEnabled ? null : UserMod.Translation.GetTranslation("CUSTOMIZE-IT-OPTION-MAINMENU-TOOLTIP");

        public void Initialize()
        {
            if (initialized) return;
            AddPanelButton();
            initialized = true;
        }

        public void Release()
        {
            initialized = false;
            inizializedButton = false;
        }

        public void SaveBuilding(BuildingInfo building)
        {
            if (!CustomBuildingData.TryGetValue(building.name, out CustomizableProperties customProperties))
                CustomBuildingData.Add(building.name, new CustomizableProperties(building));
            else CustomBuildingData[building.name] = new CustomizableProperties(building);

            if(!UserMod.Settings.SavePerCity) UserMod.Settings.Save();
        }

        public void ResetBuilding(BuildingInfo building)
        {
            var properties = building.ResetProperties();
            if (CustomBuildingData.TryGetValue(building.name, out CustomizableProperties customProperties))
                CustomBuildingData.Remove(building.name);
            building.LoadCustomProperties(properties);
            if (!UserMod.Settings.SavePerCity) UserMod.Settings.Save();
        }

        private void AddPanelButton()
        {
            if (!inizializedButton)
            {
                ServiceBuildingInfoPanel = GameObject.Find("(Library) CityServiceWorldInfoPanel").GetComponent<CityServiceWorldInfoPanel>();

                if (ServiceBuildingInfoPanel != null)
                {
                    AddBuildingPanelControls(ServiceBuildingInfoPanel, out customizeButton, new Vector3(-7f, 43f, 0f));  
                    inizializedButton = true;
                }
            }
        }

        private void AddBuildingPanelControls(WorldInfoPanel infoPanel, out UIButton button, Vector3 customizeButtonOffset)
        {
            button = UIUtil.CreateToggleButton(infoPanel.component, customizeButtonOffset, UIAlignAnchor.TopRight, delegate (UIComponent component, UIMouseEventParameter param)
            {
                InstanceID instanceID = (InstanceID)infoPanel.GetType().GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(infoPanel);
                var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;
                if (CustomizePanel == null || building != CurrentBuilding)
                    CustomizePanel = building.GenerateCustomizationPanel();
                else
                {
                    CustomizePanel.isVisible = false;
                    UIUtil.DestroyDeeply(CustomizePanel);
                }
                if(component.hasFocus) component.Unfocus();
            });
        }      
        
        internal void ToggleOptionPanelControls(bool inGame)
        {
            SavePerCityCheckBox.isEnabled = !inGame;
            ResetAllButton.isEnabled = inGame;
            ResetAllButton.tooltip = ButtonTooltip;
            SavePerCityCheckBox.tooltip = CheckBoxTooltip;
            SavePerCityCheckBox.Find<UISprite>("Unchecked").spriteName = inGame ? "ToggleBaseDisabled" : "ToggleBase";
            ((UISprite)SavePerCityCheckBox.checkedBoxObject).spriteName = inGame ? "ToggleBaseDisabled" : "ToggleBaseFocused";
        }
    }
}

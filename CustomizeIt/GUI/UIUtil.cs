using ColossalFramework.UI;
using System.Collections.Generic;
using UnityEngine;

namespace CustomizeIt.GUI
{
    public class UIUtil
    {
        public const float textFieldHeight = 23f;
        public const float textFieldWidth = 100f;
        public const float textFieldMargin = 5f;
        private static string ResetText => UserMod.Translation.GetTranslation("CUSTOMIZE-IT-RESET");

        public static Dictionary<string, string> FieldNames => UpdateTranslations();

        public static Dictionary<string, string> UpdateTranslations() {
            return new Dictionary<string, string>() {
                ["m_visitors"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_visitors"),
                ["m_productionCapacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_productionCapacity"),
                ["m_homeCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_homeCount"),
                ["m_incomeAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_incomeAccumulation"),
                ["m_constructionCost"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_constructionCost"),
                ["m_maintenanceCost"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_maintenanceCost"),
                ["m_electricityConsumption"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_electricityConsumption"),
                ["m_waterConsumption"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_waterConsumption"),
                ["m_sewageAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_sewageAccumulation"),
                ["m_garbageAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_garbageAccumulation"),
                ["m_fireHazard"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_fireHazard"),
                ["m_fireTolerance"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_fireTolerance"),
                ["m_visitPlaceCount0"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_visitPlaceCount0"),
                ["m_visitPlaceCount1"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_visitPlaceCount1"),
                ["m_visitPlaceCount2"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_visitPlaceCount2"),
                ["m_entertainmentAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_entertainmentAccumulation"),
                ["m_entertainmentRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_entertainmentRadius"),
                ["m_workPlaceCount0"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_workPlaceCount0"),
                ["m_workPlaceCount1"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_workPlaceCount1"),
                ["m_workPlaceCount2"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_workPlaceCount2"),
                ["m_workPlaceCount3"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_workPlaceCount3"),
                ["m_noiseAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_noiseAccumulation"),
                ["m_noiseRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_noiseRadius"),
                ["m_cargoTransportAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_cargoTransportAccumulation"),
                ["m_cargoTransportRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_cargoTransportRadius"),
                ["m_hearseCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_hearseCount"),
                ["m_corpseCapacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_corpseCapacity"),
                ["m_burialRate"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_burialRate"),
                ["m_graveCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_graveCount"),
                ["m_deathCareAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_deathCareAccumulation"),
                ["m_deathCareRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_deathCareRadius"),
                ["m_helicopterCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_helicopterCount"),
                ["m_vehicleCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_vehicleCount"),
                ["m_detectionRange"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_detectionRange"),
                ["m_fireDepartmentAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_fireDepartmentAccumulation"),
                ["m_fireDepartmentRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_fireDepartmentRadius"),
                ["m_fireTruckCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_fireTruckCount"),
                ["m_firewatchRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_firewatchRadius"),
                ["m_educationAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_educationAccumulation"),
                ["m_educationRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_educationRadius"),
                ["m_studentCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_studentCount"),
                ["m_resourceCapacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_resourceCapacity"),
                ["m_resourceConsumption"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_resourceConsumption"),
                ["m_heatingProduction"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_heatingProduction"),
                ["m_pollutionAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_pollutionAccumulation"),
                ["m_pollutionRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_pollutionRadius"),
                ["m_ambulanceCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_ambulanceCount"),
                ["m_patientCapacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_patientCapacity"),
                ["m_curingRate"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_curingRate"),
                ["m_healthCareAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_healthCareAccumulation"),
                ["m_healthCareRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_healthCareRadius"),
                ["m_animalCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_animalCount"),
                ["m_collectRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_collectRadius"),
                ["m_electricityProduction"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_electricityProduction"),
                ["m_garbageCapacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_garbageCapacity"),
                ["m_garbageConsumption"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_garbageConsumption"),
                ["m_garbageTruckCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_garbageTruckCount"),
                ["m_materialProduction"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_materialProduction"),
                ["m_maintenanceRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_maintenanceRadius"),
                ["m_maintenanceTruckCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_maintenanceTruckCount"),
                ["m_monumentLevel"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_monumentLevel"),
                ["m_attractivenessAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_attractivenessAccumulation"),
                ["m_animalArea"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_animalArea"),
                ["m_landValueAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_landValueAccumulation"),
                ["m_jailCapacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_jailCapacity"),
                ["m_policeCarCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_policeCarCount"),
                ["m_policeDepartmentAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_policeDepartmentAccumulation"),
                ["m_policeDepartmentRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_policeDepartmentRadius"),
                ["m_sentenceWeeks"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_sentenceWeeks"),
                ["m_batteryFactor"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_batteryFactor"),
                ["m_transmitterPower"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_transmitterPower"),
                ["m_capacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_capacity"),
                ["m_disasterCoverageAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_disasterCoverageAccumulation"),
                ["m_electricityStockpileAmount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_electricityStockpileAmount"),
                ["m_electricityStockpileRate"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_electricityStockpileRate"),
                ["m_evacuationBusCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_evacuationBusCount"),
                ["m_evacuationRange"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_evacuationRange"),
                ["m_goodsConsumptionRate"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_goodsConsumptionRate"),
                ["m_goodsStockpileAmount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_goodsStockpileAmount"),
                ["m_waterStockpileAmount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_waterStockpileAmount"),
                ["m_waterStockpileRate"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_waterStockpileRate"),
                ["m_snowCapacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_snowCapacity"),
                ["m_snowConsumption"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_snowConsumption"),
                ["m_snowTruckCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_snowTruckCount"),
                ["m_publicTransportAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_publicTransportAccumulation"),
                ["m_publicTransportRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_publicTransportRadius"),
                ["m_residentCapacity"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_residentCapacity"),
                ["m_touristFactor0"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_touristFactor0"),
                ["m_touristFactor1"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_touristFactor1"),
                ["m_touristFactor2"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_touristFactor2"),
                ["m_maxVehicleCount"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_maxVehicleCount"),
                ["m_maxVehicleCount2"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_maxVehicleCount2"),
                ["m_cleaningRate"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_cleaningRate"),
                ["m_maxWaterDistance"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_maxWaterDistance"),
                ["m_outletPollution"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_outletPollution"),
                ["m_pumpingVehicles"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_pumpingVehicles"),
                ["m_sewageOutlet"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_sewageOutlet"),
                ["m_sewageStorage"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_sewageStorage"),
                ["m_useGroundWater"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_useGroundWater"),
                ["m_vehicleRadius"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_vehicleRadius"),
                ["m_waterIntake"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_waterIntake"),
                ["m_waterOutlet"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_waterOutlet"),
                ["m_waterStorage"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_waterStorage"),
                ["m_mailAccumulation"] = UserMod.Translation.GetTranslation("CUSTOMIZE-IT-m_mailAccumulation"),
            };
        }

        public static UIButton CreateToggleButton(UIComponent parentComponent, Vector3 offset, UIAlignAnchor anchor, MouseEventHandler handler) {
            UIButton uibutton = UIView.GetAView().AddUIComponent(typeof(UIButton)) as UIButton;
            uibutton.name = "CustomizeItButton";
            uibutton.width = 26f;
            uibutton.height = 26f;
            uibutton.normalFgSprite = "Options";
            uibutton.disabledFgSprite = "OptionsDisabled";
            uibutton.hoveredFgSprite = "OptionsHovered";
            uibutton.focusedFgSprite = "OptionsFocused";
            uibutton.pressedFgSprite = "OptionsPressed";
            uibutton.normalBgSprite = "OptionBase";
            uibutton.disabledBgSprite = "OptionBaseDisabled";
            uibutton.hoveredBgSprite = "OptionBaseHovered";
            uibutton.focusedBgSprite = "OptionBaseFocused";
            uibutton.pressedBgSprite = "OptionBasePressed";
            uibutton.eventClick += handler;
            uibutton.AlignTo(parentComponent, anchor);
            uibutton.relativePosition += offset;
            return uibutton;
        }

        public static UIButton CreateResetButton(UIComponent parentComponent) {
            UIButton button = parentComponent.AddUIComponent<UIButton>();
            button.name = "CustomizeItResetButton";
            button.text = ResetText;
            button.width = textFieldWidth;
            button.height = textFieldHeight;
            button.textPadding = new RectOffset(0, 0, 5, 0);
            button.horizontalAlignment = UIHorizontalAlignment.Center;
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textScale = 0.8f;
            button.atlas = UIView.GetAView().defaultAtlas;
            button.normalBgSprite = "ButtonMenu";
            button.disabledBgSprite = "ButtonMenuDisabled";
            button.hoveredBgSprite = "ButtonMenuHovered";
            button.focusedBgSprite = "ButtonMenu";
            button.pressedBgSprite = "ButtonMenuPressed";
            button.eventClick += (c, e) => {
                var building = CustomizeIt.instance.CurrentBuilding;
                CustomizeIt.instance.ResetBuilding(building);
                var ai = building.m_buildingAI;
                foreach (var input in UICustomizePanel.Instance.Inputs) {
                    if (input is UITextField)
                        ((UITextField)input).text = ai.GetType().GetField(input.name)?.GetValue(ai)?.ToString();
                    else if (input is UICheckBox)
                        ((UICheckBox)input).isChecked = (bool)ai.GetType().GetField(input.name)?.GetValue(ai);
                }
            };
            return button;
        }
        public static void DestroyDeeply(UIComponent component) {
            if (component == null) return;

            UIComponent[] children = component.GetComponentsInChildren<UIComponent>();

            if (children != null && children.Length > 0) {
                for (int i = 0; i < children.Length; i++)
                    if (children[i].parent == component)
                        DestroyDeeply(children[i]);
            }
            Object.Destroy(component);
            component = null;
        }

        public static UICheckBox CreateCheckBox(UIComponent parent, string fieldName) {
            UICheckBox checkBox = parent.AddUIComponent<UICheckBox>();

            checkBox.name = fieldName;
            checkBox.width = 20f;
            checkBox.height = 20f;
            checkBox.relativePosition = Vector3.zero;

            UISprite sprite = checkBox.AddUIComponent<UISprite>();
            sprite.spriteName = "ToggleBase";
            sprite.size = new Vector2(16f, 16f);
            sprite.relativePosition = new Vector3(2f, 2f);

            checkBox.checkedBoxObject = sprite.AddUIComponent<UISprite>();
            ((UISprite)checkBox.checkedBoxObject).spriteName = "ToggleBaseFocused";
            checkBox.checkedBoxObject.size = new Vector2(16f, 16f);
            checkBox.checkedBoxObject.relativePosition = Vector3.zero;

            checkBox.eventCheckChanged += EventCheckChangedHandler;
            checkBox.isChecked = (bool)CustomizeIt.instance.CurrentBuilding.m_buildingAI.GetType().GetField(fieldName).GetValue(CustomizeIt.instance.CurrentBuilding.m_buildingAI);
            return checkBox;
        }

        private static void EventCheckChangedHandler(UIComponent component, bool value) {
            var ai = CustomizeIt.instance.CurrentBuilding.m_buildingAI;
            var type = ai.GetType();
            type.GetField(component.name)?.SetValue(ai, value);
        }

        public static UITextField CreateTextField(UIComponent parent, string fieldName) {
            UITextField textField = parent.AddUIComponent<UITextField>();

            textField.name = fieldName;
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;

            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);

            textField.width = textFieldWidth;
            textField.height = textFieldHeight;
            textField.padding = new RectOffset(6, 6, 6, 6);
            textField.normalBgSprite = "LevelBarBackground";
            textField.hoveredBgSprite = "LevelBarBackground";
            textField.disabledBgSprite = "LevelBarBackground";
            textField.focusedBgSprite = "LevelBarBackground";
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.textColor = Color.white;
            textField.textScale = 0.85f;
            textField.selectOnFocus = true;
            textField.eventKeyPress += EventKeyPressedHandler;
            textField.eventTextSubmitted += EventTextSubmittedHandler;
            textField.text = CustomizeIt.instance.CurrentBuilding.m_buildingAI.GetType().GetField(fieldName).GetValue(CustomizeIt.instance.CurrentBuilding.m_buildingAI).ToString();
            return textField;
        }

        private static void EventTextSubmittedHandler(UIComponent component, string value) {
            if (int.TryParse(value, out int result)) {
                var ai = CustomizeIt.instance.CurrentBuilding.m_buildingAI;
                var type = ai.GetType();
                if (component.name.ToLower().Contains("capacity") && result == 0) {
                    result = 1;
                    ((UITextField)component).text = "1";
                }
                type.GetField(component.name)?.SetValue(ai, result);
                if ((component.name.ToLower().Contains("homecount") || component.name.ToLower().Contains("placecount") || component.name.ToLower().Contains("sewage") || component.name.ToLower().Contains("garbage"))) {
                    bool isHomeOrWorkplace = component.name.ToLower().Contains("homecount") || component.name.ToLower().Contains("placecount");
                    bool isSewage = component.name.ToLower().Contains("sewage");
                    bool isGarbage = component.name.ToLower().Contains("garbage");
                    SimulationManager.instance.AddAction(() => {
                        for (ushort i = 0; i < BuildingManager.instance.m_buildings.m_buffer.Length; i++) {
                            var building = BuildingManager.instance.m_buildings.m_buffer[i];
                            if (building.m_flags == 0 || building.Info == null || building.Info != CustomizeIt.instance.CurrentBuilding) continue;
                            if (isHomeOrWorkplace) building.Info.m_buildingAI.BuildingUpgraded(i, ref BuildingManager.instance.m_buildings.m_buffer[i]);
                            if (isSewage) BuildingManager.instance.m_buildings.m_buffer[i].m_sewageBuffer = 0;
                            if (isGarbage) BuildingManager.instance.m_buildings.m_buffer[i].m_garbageBuffer = 0;
                        }
                    });
                }
            }
        }

        private static void EventKeyPressedHandler(UIComponent component, UIKeyEventParameter eventParam) {
            if (!char.IsControl(eventParam.character) && !char.IsDigit(eventParam.character))
                eventParam.Use();
        }
    }
}

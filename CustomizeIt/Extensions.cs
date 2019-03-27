using ColossalFramework.UI;
using CustomizeIt.AI;
using CustomizeIt.AI.Commercial;
using CustomizeIt.AI.Extractor;
using CustomizeIt.AI.Industrial;
using CustomizeIt.AI.Office;
using CustomizeIt.AI.Residential;
using CustomizeIt.GUI;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CustomizeIt
{
    public static class Extensions
    {
        public static void LoadCustomProperties(this BuildingInfo building, CustomizableProperties properties) {
            var ai = building.m_buildingAI;
            var buildingFields = ai.GetType().GetFields();
            var customFields = properties.GetType().GetFields();
            var fields = new Dictionary<string, FieldInfo>();
            foreach (var customField in customFields)
                fields.Add(customField.Name, customField);

            foreach (var buildingField in buildingFields) {
                try {
                    if (fields.TryGetValue(buildingField.Name, out FieldInfo fieldInfo))
                        buildingField.SetValue(ai, fieldInfo.GetValue(properties));
                } catch (Exception e) {
                    Debug.LogWarning(e);
                }
            }
        }

        public static UIPanelWrapper GenerateCustomizationPanel(this BuildingInfo building) {
            CustomizeIt.instance.CurrentBuilding = building;
            UIUtil.DestroyDeeply(UIView.Find("CustomizeItPanelWrapper"));
            UIPanelWrapper wrapper = UIView.GetAView().AddUIComponent(typeof(UIPanelWrapper)) as UIPanelWrapper;
            return wrapper;
        }
        public static CustomizableProperties GetCustomizableProperties(this BuildingInfo building) {
            return new CustomizableProperties(building);
        }

        public static CustomizableProperties ResetProperties(this BuildingInfo building) {
            if (CustomizeIt.instance.OriginalBuildingData.TryGetValue(building.name, out CustomizableProperties properties))
                return properties;
            return null;
        }

        public static void Convert(this BuildingInfo building) {
            try {
                if (building == null || building.m_buildingAI == null || building.m_mesh == null) return;
                if (building.m_buildingAI.GetType().IsSubclassOf(typeof(PrivateBuildingAI)) || building.m_mesh.name.ToLower().Contains("customizable")) {
                    PrivateBuildingAI newAI;
                    if (building.m_buildingAI.GetType() == typeof(ResidentialBuildingAI) || building.m_buildingAI.GetType().IsSubclassOf(typeof(ResidentialBuildingAI)) || building.m_mesh.name.ToLower().Contains("residential"))
                        newAI = building.gameObject.AddComponent<CustomizableResidentialBuildingAI>();

                    else if (building.m_buildingAI.GetType() == typeof(CommercialBuildingAI) || building.m_buildingAI.GetType().IsSubclassOf(typeof(CommercialBuildingAI)) || building.m_mesh.name.ToLower().Contains("commercial"))
                        newAI = building.gameObject.AddComponent<CustomizableCommercialBuildingAI>();

                    else if (building.m_buildingAI.GetType() == typeof(OfficeBuildingAI) || building.m_buildingAI.GetType().IsSubclassOf(typeof(OfficeBuildingAI)) || building.m_mesh.name.ToLower().Contains("office"))
                        newAI = building.gameObject.AddComponent<CustomizableOfficeBuildingAI>();

                    else if (building.m_buildingAI.GetType() == typeof(IndustrialBuildingAI) || building.m_buildingAI.GetType().IsSubclassOf(typeof(IndustrialBuildingAI)) || building.m_mesh.name.ToLower().Contains("industrial"))
                        newAI = building.gameObject.AddComponent<CustomizableIndustrialBuildingAI>();

                    else newAI = building.gameObject.AddComponent<CustomizableIndustrialExtractorAI>();

                    var oldAI = building.m_buildingAI;
                    bool isPloppable = oldAI.GetType().Name.ToLower().Contains("ploppable") || building.m_mesh.name.ToLower().Contains("customizable");
                    newAI.m_info = building;
                    newAI.m_constructionTime = oldAI.GetType().GetField("m_constructionTime") == null || isPloppable ? 0 : (int)oldAI.GetType().GetField("m_constructionTime").GetValue(oldAI);
                    newAI.m_ignoreNoPropsWarning = oldAI.GetType().GetField("m_ignoreNoPropsWarning") != null ? (bool)oldAI.GetType().GetField("m_ignoreNoPropsWarning").GetValue(oldAI) : true;
                    if (isPloppable && !CustomizeIt.instance.RICOBuildings.Contains(building.name)) CustomizeIt.instance.RICOBuildings.Add(building.name);
                    ((ICustomAI)newAI).Initialize(isPloppable);
                    building.m_buildingAI = newAI;
                }
                CustomizeIt.instance.Customize(building);
            } catch (Exception ex) {
                Debug.LogWarning(ex);
            }
        }

        public static void FixFlags(ref this Building buildingData) {
            buildingData.m_garbageBuffer = 100;
            buildingData.m_majorProblemTimer = 0;
            buildingData.m_levelUpProgress = 0;
            buildingData.m_flags &= ~Building.Flags.ZonesUpdated;
            buildingData.m_flags &= ~Building.Flags.Abandoned;
            buildingData.m_flags &= ~Building.Flags.Demolishing;
            buildingData.m_problems &= ~Notification.Problem.TurnedOff;
        }
    }
}

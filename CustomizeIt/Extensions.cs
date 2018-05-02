using ColossalFramework.UI;
using CustomizeIt.GUI;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CustomizeIt
{
    public static class Extensions
    {
        public static void LoadCustomProperties(this BuildingInfo building, CustomizableProperties properties)
        {
            var ai = building.m_buildingAI;
            var buildingFields = ai.GetType().GetFields();
            var customFields = properties.GetType().GetFields();
            var fields = new Dictionary<string, FieldInfo>();
            foreach (var customField in customFields)
                fields.Add(customField.Name, customField);

            foreach (var buildingField in buildingFields)
            {
                try
                {
                    if (fields.TryGetValue(buildingField.Name, out FieldInfo fieldInfo))
                        buildingField.SetValue(ai, fieldInfo.GetValue(properties));
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
            }
        }

        public static UIPanelWrapper GenerateCustomizationPanel(this BuildingInfo building)
        {
            CustomizeIt.instance.CurrentBuilding = building;
            UIUtil.DestroyDeeply(UIView.Find("CustomizeItPanelWrapper"));
            UIPanelWrapper wrapper = UIView.GetAView().AddUIComponent(typeof(UIPanelWrapper)) as UIPanelWrapper;            
            return wrapper;
        }
        public static CustomizableProperties GetCustomizableProperties(this BuildingInfo building)
        {
            return new CustomizableProperties(building);
        }

        public static CustomizableProperties ResetProperties(this BuildingInfo building)
        {
            if (CustomizeIt.instance.OriginalBuildingData.TryGetValue(building.name, out CustomizableProperties properties))
                return properties;
            return null;
        }        
    }
}

using ColossalFramework.UI;
using System.Reflection;
using UnityEngine;

namespace CustomizeIt.GUI
{
    public class UIPanelWrapper : UIPanel
    {
        private UITitleBar titleBar;
        private UICustomizePanel customizePanel;
        public static UIPanelWrapper Instance;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }
        public override void Update()
        {
            base.Update();
            InstanceID instanceID = (InstanceID)CustomizeIt.instance.ServiceBuildingInfoPanel.GetType().GetField("m_InstanceID", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(CustomizeIt.instance.ServiceBuildingInfoPanel);
            var building = BuildingManager.instance.m_buildings.m_buffer[instanceID.Building].Info;
            if (building != CustomizeIt.instance.CurrentBuilding)
            {
                UIUtil.DestroyDeeply(this);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            CustomizeIt.instance.SaveBuilding(CustomizeIt.instance.CurrentBuilding);
        }
        private void SetupControls()
        {
            isVisible = false;
            isInteractive = true;
            name = "CustomizeItPanelWrapper";
            padding = new RectOffset(10, 10, 4, 4);
            relativePosition = new Vector3(10, 60);
            backgroundSprite = "MenuPanel";
            titleBar = AddUIComponent<UITitleBar>();
            customizePanel = AddUIComponent<UICustomizePanel>();
        }
    }
}

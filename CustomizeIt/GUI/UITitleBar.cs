using ColossalFramework.UI;
using UnityEngine;

namespace CustomizeIt.GUI
{
    public class UITitleBar : UIPanel
    {
        public static UITitleBar Instance;
        private UILabel titleLabel;
        private UIButton closeButton;
        public UIDragHandle dragHandle;

        public override void Start()
        {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls()
        {
            name = "CustomizeItTitleBar";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = Vector3.zero;
            width = parent.width;
            height = 40f;

            dragHandle = AddUIComponent<UIDragHandle>();
            dragHandle.height = height;
            dragHandle.relativePosition = Vector3.zero;
            dragHandle.target = parent;

            titleLabel = AddUIComponent<UILabel>();
            titleLabel.text = CustomizeIt.instance.CurrentBuilding.GetUncheckedLocalizedTitle();
            titleLabel.textScale = 0.9f;
            titleLabel.isInteractive = false;

            closeButton = AddUIComponent<UIButton>();
            closeButton.size = new Vector2(20, 20);
            closeButton.relativePosition = new Vector3(width - closeButton.width - 10f, 10f);
            closeButton.normalBgSprite = "DeleteLineButton";
            closeButton.hoveredBgSprite = "DeleteLineButtonHovered";
            closeButton.pressedBgSprite = "DeleteLineButtonPressed";
            closeButton.eventClick += (component, param) =>
            {
                CustomizeIt.instance.CustomizePanel.isVisible = false;
                UIUtil.DestroyDeeply(CustomizeIt.instance.CustomizePanel);                
            };
        }

        public void RecenterElements()
        {
            closeButton.relativePosition = new Vector3(width - closeButton.width - 10f, 10f);
            titleLabel.relativePosition = new Vector3((width - titleLabel.width) / 2f, (height - titleLabel.height) / 2);
        }
    }
}

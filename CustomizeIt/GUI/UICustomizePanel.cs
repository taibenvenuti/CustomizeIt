using ColossalFramework.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomizeIt.GUI
{
    public class UICustomizePanel : UIPanel
    {
        private BuildingInfo Building => CustomizeIt.instance.CurrentBuilding;
        internal static UICustomizePanel Instance;
        private List<UILabel> labels;
        internal List<UIComponent> Inputs;

        public override void Start() {
            base.Start();
            Instance = this;
            SetupControls();
        }

        private void SetupControls() {
            name = "CustomizeItPanel";
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            relativePosition = new Vector3(0f, UITitleBar.Instance.height);
            width = parent.width;

            var ai = Building.m_buildingAI;
            var type = ai.GetType();
            var fields = type.GetFields();
            var fieldsToGet = typeof(CustomizableProperties).GetFields().Select(f => f.Name);
            Inputs = new List<UIComponent>();
            labels = new List<UILabel>();
            float widest = 0;
            foreach (var field in fields.Where(f => fieldsToGet.Contains(f.Name))) {
                var label = AddUIComponent<UILabel>();
                label.name = field.Name + "Label";
                label.text = UIUtil.FieldNames[field.Name];
                label.textScale = 0.9f;
                label.isInteractive = false;
                if (field.FieldType == typeof(int) || field.FieldType == typeof(float)) {
                    Inputs.Add(UIUtil.CreateTextField(this, field.Name));
                    labels.Add(label);
                } else if (field.FieldType == typeof(bool)) {
                    Inputs.Add(UIUtil.CreateCheckBox(this, field.Name));
                    labels.Add(label);
                }
                if ((label.width + UIUtil.textFieldWidth + (UIUtil.textFieldMargin * 6)) > widest)
                    widest = label.width + UIUtil.textFieldWidth + (UIUtil.textFieldMargin * 6);
            }
            Inputs.Sort((x, y) => x.name.CompareTo(y.name));
            labels.Sort((x, y) => x.name.CompareTo(y.name));
            Inputs.Add(UIUtil.CreateResetButton(this));
            width = UIPanelWrapper.Instance.width = UITitleBar.Instance.width = UITitleBar.Instance.dragHandle.width = widest;
            UITitleBar.Instance.RecenterElements();
            AlignChildren();
            height = (Inputs.Count * (UIUtil.textFieldHeight + UIUtil.textFieldMargin)) + (UIUtil.textFieldMargin * 3);
            UIPanelWrapper.Instance.height = height + UITitleBar.Instance.height;
            UIPanelWrapper.Instance.relativePosition = new Vector3(UserMod.Settings.PanelX, UserMod.Settings.PanelY);
            isVisible = UIPanelWrapper.Instance.isVisible = UITitleBar.Instance.isVisible = UITitleBar.Instance.dragHandle.isVisible = true;
        }

        private void AlignChildren() {
            float inputX = width - UIUtil.textFieldWidth - (UIUtil.textFieldMargin * 2);

            for (int i = 0; i < Inputs.Count; i++) {
                float finalY = (i * UIUtil.textFieldHeight) + ((UIUtil.textFieldMargin) * (i + 2));

                if (i < labels.Count) {
                    float labelX = inputX - labels[i].width - (UIUtil.textFieldMargin * 2);
                    labels[i].relativePosition = new Vector3(labelX, finalY + 4);
                }
                Inputs[i].relativePosition = Inputs[i] is UICheckBox ? new Vector3(inputX + ((UIUtil.textFieldWidth - Inputs[i].width) / 2), finalY + ((UIUtil.textFieldHeight - Inputs[i].height) / 2)) : new Vector3(inputX, finalY);
            }
        }
    }
}

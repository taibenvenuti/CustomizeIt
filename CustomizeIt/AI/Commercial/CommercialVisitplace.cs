﻿using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Commercial
{
    public partial class CustomizableCommercialBuildingAI : CommercialBuildingAI, ICustomAI
    {
        public override int CalculateVisitplaceCount(ItemClass.Level level, Randomizer r, int width, int length) {
            return m_visitors;
        }

        private void InitVisitplaces() {
            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return visitors;
            }
            var r = new Randomizer(m_info.m_prefabDataIndex);
            int num = 0;
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            var width = m_info.m_cellWidth;
            var length = m_info.m_cellLength;
            if (subService != ItemClass.SubService.CommercialLow) {
                if (subService != ItemClass.SubService.CommercialHigh) {
                    if (subService != ItemClass.SubService.CommercialLeisure) {
                        if (subService != ItemClass.SubService.CommercialTourist) {
                            if (subService == ItemClass.SubService.CommercialEco) {
                                num = 100;
                            }
                        } else {
                            num = 250;
                        }
                    } else {
                        num = 250;
                    }
                } else if (level == ItemClass.Level.Level1) {
                    num = 200;
                } else if (level == ItemClass.Level.Level2) {
                    num = 300;
                } else {
                    num = 400;
                }
            } else if (level == ItemClass.Level.Level1) {
                num = 90;
            } else if (level == ItemClass.Level.Level2) {
                num = 100;
            } else {
                num = 110;
            }
            if (num != 0) {
                num = Mathf.Max(200, width * length * num + r.Int32(100u)) / 100;
            }
            m_visitors = num;
        }
    }
}

using ColossalFramework.Math;

namespace CustomizeIt.AI.Commercial
{
    public partial class CustomizableCommercialBuildingAI : CommercialBuildingAI, ICustomAI
    {
        public override void GetPollutionRates(ItemClass.Level level, int productionRate, DistrictPolicies.CityPlanning cityPlanningPolicies, out int groundPollution, out int noisePollution) {
            groundPollution = 0;
            var noise = (cityPlanningPolicies & DistrictPolicies.CityPlanning.NoLoudNoises) != DistrictPolicies.CityPlanning.None ? m_noiseAccumulation / 2 : m_noiseAccumulation;
            noisePollution = (productionRate * noise + 99) / 100;
        }

        private void InitPollution() {
            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return;
            }

            var r = new Randomizer(m_info.m_prefabDataIndex);
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService != ItemClass.SubService.CommercialLow) {
                if (subService != ItemClass.SubService.CommercialHigh) {
                    if (subService != ItemClass.SubService.CommercialLeisure) {
                        if (subService != ItemClass.SubService.CommercialTourist) {
                            if (subService == ItemClass.SubService.CommercialEco) {
                                m_noiseAccumulation = 0;
                            }
                        } else {
                            m_noiseAccumulation = 150;
                        }
                    } else {
                        m_noiseAccumulation = 300;
                    }
                } else {
                    if (level != ItemClass.Level.Level1) {
                        if (level != ItemClass.Level.Level2) {
                            if (level == ItemClass.Level.Level3) {
                                m_noiseAccumulation = 150;
                            }
                        } else {
                            m_noiseAccumulation = 175;
                        }
                    } else {
                        m_noiseAccumulation = 200;
                    }
                }
            } else {
                if (level != ItemClass.Level.Level1) {
                    if (level != ItemClass.Level.Level2) {
                        if (level == ItemClass.Level.Level3) {
                            m_noiseAccumulation = 100;
                        }
                    } else {
                        m_noiseAccumulation = 120;
                    }
                } else {
                    m_noiseAccumulation = 140;
                }
            }
        }
    }
}

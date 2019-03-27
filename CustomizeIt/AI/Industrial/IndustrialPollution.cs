using ColossalFramework.Math;

namespace CustomizeIt.AI.Industrial
{
    public partial class CustomizableIndustrialBuildingAI : IndustrialBuildingAI, ICustomAI
    {
        public override void GetPollutionRates(ItemClass.Level level, int productionRate, DistrictPolicies.CityPlanning cityPlanningPolicies, out int groundPollution, out int noisePollution) {
            groundPollution = m_pollutionAccumulation;
            noisePollution = m_noiseAccumulation;
            if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.NoLoudNoises) != DistrictPolicies.CityPlanning.None) {
                noisePollution = noisePollution / 2;
            }
            if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.FilterIndustrialWaste) != DistrictPolicies.CityPlanning.None) {
                groundPollution = groundPollution + 1 >> 1;
            }
            groundPollution = (productionRate * groundPollution + 99) / 100;
            noisePollution = (productionRate * noisePollution + 99) / 100;
        }

        public void InitPollution() {
            if (UserMod.Settings.UseRPCValues || m_isPloppable) {
                //return;
            }

            var r = new Randomizer(m_info.m_prefabDataIndex);
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService == ItemClass.SubService.IndustrialGeneric) {
                if (level != ItemClass.Level.Level1) {
                    if (level != ItemClass.Level.Level2) {
                        if (level == ItemClass.Level.Level3) {
                            m_pollutionAccumulation = 150;
                            m_noiseAccumulation = 150;
                        }
                    } else {
                        m_pollutionAccumulation = 200;
                        m_noiseAccumulation = 200;
                    }
                } else {
                    m_pollutionAccumulation = 300;
                    m_noiseAccumulation = 300;
                }
            } else if (subService == ItemClass.SubService.IndustrialOre) {
                m_pollutionAccumulation = 400;
                m_noiseAccumulation = 500;
            } else if (subService == ItemClass.SubService.IndustrialOil) {
                m_pollutionAccumulation = 500;
                m_noiseAccumulation = 400;
            } else if (subService == ItemClass.SubService.IndustrialForestry) {
                m_pollutionAccumulation = 0;
                m_noiseAccumulation = 200;
            } else if (subService == ItemClass.SubService.IndustrialFarming) {
                m_pollutionAccumulation = 0;
                m_noiseAccumulation = 200;
            }
        }
    }
}

using ColossalFramework.Math;
using UnityEngine;

namespace CustomizeIt.AI.Extractor
{
    public partial class CustomizableIndustrialExtractorAI : IndustrialExtractorAI, ICustomAI
    {
        public override void GetPollutionRates(int productionRate, DistrictPolicies.CityPlanning cityPlanningPolicies, out int groundPollution, out int noisePollution)
        {
            groundPollution = m_pollutionAccumulation;
            noisePollution = m_noiseAccumulation;
            if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.NoLoudNoises) != DistrictPolicies.CityPlanning.None)
            {
                noisePollution = noisePollution / 2;
            }
            if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.FilterIndustrialWaste) != DistrictPolicies.CityPlanning.None)
            {
                groundPollution = groundPollution + 1 >> 1;
            }
            groundPollution = (productionRate * groundPollution + 99) / 100;
            noisePollution = (productionRate * noisePollution + 99) / 100;
        }

        public void GetPollution(out int groundPollution, out int noisePollution)
        {
            groundPollution = 0;
            noisePollution = 0;

            if (UserMod.Settings.UseRPCValues || m_isPloppable)
            {
                var lvl = m_info.m_class.m_level >= 0 ? m_info.m_class.m_level : 0;
                var array = GetArray(m_info, lvl);
                groundPollution = array[RPCData.GROUND_POLLUTION];
                noisePollution = array[RPCData.NOISE_POLLUTION];
                return;
            }

            var r = new Randomizer(GetHashCode());
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService == ItemClass.SubService.IndustrialGeneric)
            {
                if (level != ItemClass.Level.Level1)
                {
                    if (level != ItemClass.Level.Level2)
                    {
                        if (level == ItemClass.Level.Level3)
                        {
                            groundPollution = 150;
                            noisePollution = 150;
                        }
                    }
                    else
                    {
                        groundPollution = 200;
                        noisePollution = 200;
                    }
                }
                else
                {
                    groundPollution = 300;
                    noisePollution = 300;
                }
            }
            else if (subService == ItemClass.SubService.IndustrialOre)
            {
                groundPollution = 400;
                noisePollution = 500;
            }
            else if (subService == ItemClass.SubService.IndustrialOil)
            {
                groundPollution = 500;
                noisePollution = 400;
            }
            else if (subService == ItemClass.SubService.IndustrialForestry)
            {
                groundPollution = 0;
                noisePollution = 200;
            }
            else if (subService == ItemClass.SubService.IndustrialFarming)
            {
                groundPollution = 0;
                noisePollution = 200;
            }
        }
    }
}

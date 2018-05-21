using ColossalFramework.Math;

namespace CustomizeIt.AI.Commercial
{
    public partial class CustomizableCommercialBuildingAI : CommercialBuildingAI, ICustomAI
    {
        public override void GetPollutionRates(int productionRate, DistrictPolicies.CityPlanning cityPlanningPolicies, out int groundPollution, out int noisePollution)
        {
            groundPollution = 0;
            var noise = (cityPlanningPolicies & DistrictPolicies.CityPlanning.NoLoudNoises) != DistrictPolicies.CityPlanning.None ? m_noiseAccumulation / 2 : m_noiseAccumulation;
            noisePollution = (productionRate * noise + 99) / 100;
        }

        private void GetPollution(out int noisePollution)
        {
            noisePollution = 0;

            if (UserMod.Settings.UseRPCValues || m_isPloppable)
            {
                var lvl = m_info.m_class.m_level >= 0 ? m_info.m_class.m_level : 0;
                var array = GetArray(m_info, lvl);
                noisePollution = array[RPCData.NOISE_POLLUTION];
                return;
            }

            var r = new Randomizer(GetHashCode());
            var subService = m_info.m_class.m_subService;
            var level = m_info.m_class.m_level;
            if (subService != ItemClass.SubService.CommercialLow)
            {
                if (subService != ItemClass.SubService.CommercialHigh)
                {
                    if (subService != ItemClass.SubService.CommercialLeisure)
                    {
                        if (subService != ItemClass.SubService.CommercialTourist)
                        {
                            if (subService == ItemClass.SubService.CommercialEco)
                            {
                                noisePollution = 0;
                            }
                        }
                        else
                        {
                            noisePollution = 150;
                        }
                    }
                    else
                    {
                        noisePollution = 300;
                    }
                }
                else
                {
                    if (level != ItemClass.Level.Level1)
                    {
                        if (level != ItemClass.Level.Level2)
                        {
                            if (level == ItemClass.Level.Level3)
                            {
                                noisePollution = 150;
                            }
                        }
                        else
                        {
                            noisePollution = 175;
                        }
                    }
                    else
                    {
                        noisePollution = 200;
                    }
                }
            }
            else
            {
                if (level != ItemClass.Level.Level1)
                {
                    if (level != ItemClass.Level.Level2)
                    {
                        if (level == ItemClass.Level.Level3)
                        {
                            noisePollution = 100;
                        }
                    }
                    else
                    {
                        noisePollution = 120;
                    }
                }
                else
                {
                    noisePollution = 140;
                }
            }
        }
    }
}

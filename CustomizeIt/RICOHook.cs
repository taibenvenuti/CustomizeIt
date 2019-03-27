using Harmony;
using PloppableRICO;

namespace CustomizeIt
{
    [HarmonyPatch(typeof(ConvertPrefabs), "run")]
    class PloppableResidentialPatch
    {
        static void Postfix() {
            SimulationManager.instance.AddAction(() => {
                for (uint i = 0; i < PrefabCollection<BuildingInfo>.LoadedCount(); i++) {
                    var building = PrefabCollection<BuildingInfo>.GetLoaded(i);
                    if (building.m_buildingAI.GetType().Name.ToLower().Contains("ploppable")) {
                        building.Convert();
                    }
                }
            });
        }
    }
}

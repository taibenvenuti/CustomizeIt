using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using PrefabHook;
using System.Linq;

namespace CustomizeIt
{
    public class Loading : LoadingExtensionBase
    {
        private bool done;
        private CustomizeIt Instance => CustomizeIt.instance; 

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            if (!IsHooked() || loading.currentMode != AppMode.Game) return;
            BuildingInfoHook.OnPostInitialization += OnPostBuildingInit;
            BuildingInfoHook.Deploy();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            if (mode == LoadMode.NewAsset || mode == LoadMode.LoadAsset || mode == LoadMode.NewMap || mode == LoadMode.LoadMap || mode == LoadMode.NewTheme || mode == LoadMode.LoadTheme) return;
            Instance.ToggleOptionPanelControls(true);
            if (!IsHooked())
            {
                UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                    "Missing dependency",
                    $"{Instance.name} requires the 'Prefab Hook' mod to work properly. Please subscribe to the mod and restart the game!",
                    false);
                return;
            }
            while (!done)
            {
                if (LoadingManager.instance.m_loadingComplete)
                {
                    CustomizeIt.instance.Initialize();
                    done = true;
                }
            }            
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            if (!IsHooked()) return;
            done = false;
            CustomizeIt.instance.Release();
        }

        public override void OnReleased()
        {
            base.OnReleased();
            Instance.ToggleOptionPanelControls(false);
            if (!IsHooked()) return;
            BuildingInfoHook.OnPostInitialization -= OnPostBuildingInit;
            BuildingInfoHook.Revert();
        }        
        
        public void OnPostBuildingInit(BuildingInfo building)
        {
            if (building == null || building.m_buildingAI == null || !(building.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI)))) return;
            if (!CustomizeIt.instance.OriginalBuildingData.TryGetValue(building.name, out CustomizableProperties originalProperties))
                CustomizeIt.instance.OriginalBuildingData.Add(building.name, building.GetCustomizableProperties());
            LoadCustomData(building);
        }

        internal static void LoadCustomData(BuildingInfo building)
        {
            if (CustomizeIt.instance.CustomBuildingData.TryGetValue(building.name, out CustomizableProperties customProperties))
                building.LoadCustomProperties(customProperties);
        }

        public static bool IsHooked()
        {
            var plugins = PluginManager.instance.GetPluginsInfo();
            return (from plugin in plugins.Where(p => p.isEnabled)
                    select plugin.GetInstances<IUserMod>() into instances
                    where instances.Any()
                    select instances[0].Name into name
                    where name == "Prefab Hook"
                    select name).Any();
        }
    }
}

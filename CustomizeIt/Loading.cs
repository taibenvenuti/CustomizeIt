using ColossalFramework.Plugins;
using ColossalFramework.UI;
using ICities;
using PrefabHook;

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
            UserMod.Settings = CustomizeItSettings.Load();            
            BuildingInfoHook.OnPostInitialization += OnPostBuildingInit;
            BuildingInfoHook.Deploy();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            if (mode == LoadMode.NewAsset || mode == LoadMode.LoadAsset || mode == LoadMode.NewMap || mode == LoadMode.LoadMap || mode == LoadMode.NewTheme || mode == LoadMode.LoadTheme) return;
            if (!IsHooked())
            {
                UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                    "Missing dependency",
                    $"{UserMod.name} requires the 'Prefab Hook' mod to work properly. Please subscribe to the mod and restart the game!",
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
            if (!IsHooked()) return;
            BuildingInfoHook.OnPostInitialization -= OnPostBuildingInit;
            BuildingInfoHook.Revert();
        }        
        
        public void OnPostBuildingInit(BuildingInfo building)
        {
            if (building == null || building.m_buildingAI == null || !(building.m_buildingAI.GetType().IsSubclassOf(typeof(PlayerBuildingAI)))) return;
            if (!CustomizeIt.OriginalBuildingData.TryGetValue(building.name, out CustomizableProperties originalProperties))
                CustomizeIt.OriginalBuildingData.Add(building.name, building.GetCustomizableProperties());
            if (CustomizeIt.CustomBuildingData.TryGetValue(building.name, out CustomizableProperties customProperties))
                building.LoadCustomProperties(customProperties);
        }

        internal static bool IsHooked()
        {
            foreach (PluginManager.PluginInfo current in PluginManager.instance.GetPluginsInfo())
                if (current.publishedFileID.AsUInt64 == 530771650uL || current.name == "PrefabHook") return true;
            return false;
        }        
    }
}

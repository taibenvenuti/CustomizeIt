using ColossalFramework.Plugins;
using ColossalFramework.UI;
using Harmony;
using ICities;
using PrefabHook;
using System.Linq;
using System.Reflection;

namespace CustomizeIt
{
    public class Loading : LoadingExtensionBase
    {
        private AppMode appMode;
        private bool done;
        private CustomizeIt Instance => CustomizeIt.instance; 

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            appMode = loading.currentMode;
            if (!IsHooked() || loading.currentMode != AppMode.Game) return;
            //if (Util.IsRICOActive())
            //{
            //    var harmony = HarmonyInstance.Create("com.tpb.customizeit");
            //    harmony.PatchAll(Assembly.GetExecutingAssembly());
            //}
            BuildingInfoHook.OnPostInitialization += OnPostBuildingInit;
            BuildingInfoHook.Deploy();
        }        

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            if (appMode != AppMode.Game) return;
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
            building.Convert();
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

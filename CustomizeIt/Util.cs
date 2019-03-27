using ColossalFramework.Plugins;
using ICities;
using System.Linq;

namespace CustomizeIt
{
    public class Util
    {
        public static bool IsRICOActive() {
            return IsModActive(586012417uL);
        }

        public static bool IsRPCActive() {
            return IsModActive(426163185uL);
        }

        public static bool IsModActive(ulong id) {
            var plugins = PluginManager.instance.GetPluginsInfo();
            return (from plugin in plugins.Where(p => p.isEnabled && p.publishedFileID.AsUInt64 == id)
                        select plugin).Any();
        }

        public static bool IsModActive(string modName) {
            var plugins = PluginManager.instance.GetPluginsInfo();
            return (from plugin in plugins.Where(p => p.isEnabled)
                    select plugin.GetInstances<IUserMod>() into instances
                    where instances.Any()
                    select instances[0].Name into name
                    where name == modName
                    select name).Any();
        }
    }
}

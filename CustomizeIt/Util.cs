using ColossalFramework.Plugins;
using ICities;
using System.Linq;
using System.Reflection;

namespace CustomizeIt
{
    public class Util
    {
        public static bool IsRICOActive()
        {
            return IsModActive("Ploppable RICO");
        }

        public static bool IsModActive(string modName)
        {
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

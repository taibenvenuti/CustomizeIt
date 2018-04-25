using ColossalFramework.UI;
using CustomizeIt.TranslationFramework;
using ICities;

namespace CustomizeIt
{
    public class UserMod : IUserMod
    {
        public static Translation Translation = new Translation();
        public static string name = "Customize It!";
        public string Name => name;
        public string Description
        {
            get
            {
                if (!Loading.IsHooked())
                {
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                        "Missing dependency",
                        $"{name} requires the 'Prefab Hook' mod to work properly. Please subscribe to the mod and restart the game!",
                        false);
                }
                return Translation.GetTranslation("CUSTOMIZE-IT-MODDESCRIPTION");
            }
        }

        private static CustomizeItSettings settings;
        public static CustomizeItSettings Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = CustomizeItSettings.Load();
                    if (settings == null)
                    {
                        settings = new CustomizeItSettings();
                        settings.Save();
                    }
                }
                return settings;
            }
            set
            {
                settings = value;
            }
        }
    }
}

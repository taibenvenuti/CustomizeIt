using ColossalFramework.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace CustomizeIt
{
    [XmlRoot("CustomizeIt")]
    public class CustomizeItSettings
    {
        [XmlIgnore]
        private static readonly string configurationPath = Path.Combine(DataLocation.localApplicationData, "CustomizeIt.xml");
        public List<CustomizablePropertiesEntry> Entries = new List<CustomizablePropertiesEntry>();
        public float PanelX = 8f;
        public float PanelY = 65f;
        public bool SavePerCity = false;
        public bool UseRPCValues = false;
        public CustomizeItSettings() { }
        public void OnPreSerialize() { }
        public void OnPostDeserialize() { }

        public void Save()
        {
            if (!UserMod.Settings.SavePerCity)
            {
                Entries.Clear();

                foreach (var entry in CustomizeIt.instance.CustomBuildingData)
                    if (entry.Value != null)
                        Entries.Add(entry);
            }            

            var fileName = configurationPath;
            var config = UserMod.Settings;
            var serializer = new XmlSerializer(typeof(CustomizeItSettings));

            using (var writer = new StreamWriter(fileName))
            {
                config.OnPreSerialize();
                serializer.Serialize(writer, config);
            }
        }

        public static CustomizeItSettings Load()
        {
            var fileName = configurationPath;
            var serializer = new XmlSerializer(typeof(CustomizeItSettings));

            try
            {
                using (var reader = new StreamReader(fileName))
                {
                    var config = serializer.Deserialize(reader) as CustomizeItSettings;
                    if (!config.SavePerCity)
                    {
                        var collection = CustomizeIt.instance.CustomBuildingData;
                        collection.Clear();

                        foreach (var entry in config.Entries)
                            if (entry != null)
                                collection.Add(entry.Key, entry.Value);
                    }
                    return config;
                }
            }
            catch (Exception)
            {
                return new CustomizeItSettings();
            }
        }
    }
}

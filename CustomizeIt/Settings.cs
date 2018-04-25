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
        public CustomizeItSettings() { }
        public void OnPreSerialize() { }
        public void OnPostDeserialize() { }

        public void Save()
        {
            Entries.Clear();

            foreach (var entry in CustomizeIt.CustomBuildingData)
                if (entry.Value != null)
                    Entries.Add(entry);

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
                    var collection = CustomizeIt.CustomBuildingData;
                    collection.Clear();

                    foreach (var entry in config.Entries)
                        if (entry != null)
                            collection.Add(entry.Key, entry.Value);
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

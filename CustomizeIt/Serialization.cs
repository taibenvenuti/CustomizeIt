using ICities;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CustomizeIt
{
    public class Serialization : SerializableDataExtensionBase
    {
        private CustomizeIt Instance => CustomizeIt.instance;
        private static readonly string m_dataID = "CUSTOMIZE-IT-DATA";

        private List<CustomizablePropertiesEntry> CustomBuildingDataList {
            get {
                var list = new List<CustomizablePropertiesEntry>();
                if (Instance.CustomBuildingData != null)
                    foreach (var item in Instance.CustomBuildingData)
                        list.Add(item);
                return list;
            }
            set {
                var collection = new Dictionary<string, CustomizableProperties>();
                if (value != null)
                    foreach (var item in value)
                        collection.Add(item.Key, item.Value);
                Instance.CustomBuildingData = collection;
            }
        }


        public override void OnSaveData() {
            base.OnSaveData();
            if (!UserMod.Settings.SavePerCity || Instance.CustomBuildingData == null) return;

            using (var memoryStream = new MemoryStream()) {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, CustomBuildingDataList);
                serializableDataManager.SaveData(m_dataID, memoryStream.ToArray());
            }
        }

        public override void OnLoadData() {
            base.OnLoadData();
            if (!UserMod.Settings.SavePerCity) return;
            var data = serializableDataManager.LoadData(m_dataID);
            if (data == null || data.Length == 0) return;
            var binaryFormatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream(data)) {
                CustomBuildingDataList = binaryFormatter.Deserialize(memoryStream) as List<CustomizablePropertiesEntry>;
            }
        }
    }
}

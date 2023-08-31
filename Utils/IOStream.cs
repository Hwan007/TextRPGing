using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using TextRPGing.Model;

namespace TextRPGing.Utils
{
    public class IOStream
    {
        private const string PLAYER_FILE_PATH = "../../../Data/PlayerData.json";
        private const string STORE_ITEM_FILE_PATH = "../../../Data/StoreItem.json";

        private const string DATA_FILE_DIR = "../../../Data/";

        public static void SavePlayerJsonFile()
        {
            string jsonText = JsonConvert.SerializeObject(Character.Player);

            DirectoryInfo di = new DirectoryInfo(DATA_FILE_DIR);
            if (di.Exists)
            {
                File.WriteAllText(PLAYER_FILE_PATH, jsonText);
            }
        }

        public static Character LoadPlayerJsonFile()
        {
            Character player = null;
            DirectoryInfo di = new DirectoryInfo(DATA_FILE_DIR);
            if (di.Exists && File.Exists(PLAYER_FILE_PATH))
            {
                string json = File.ReadAllText(PLAYER_FILE_PATH);
                player = JsonConvert.DeserializeObject<Character>(json);
            }

            return player;
        }

        public static List<Item> LoadStoreItem()
        {
            List<Item> items = null;
            DirectoryInfo di = new DirectoryInfo(DATA_FILE_DIR);
            if (di.Exists && File.Exists(STORE_ITEM_FILE_PATH))
            {
                string json = File.ReadAllText(STORE_ITEM_FILE_PATH);
                items = JsonConvert.DeserializeObject<List<Item>>(json);
            }

            return items;
        }
    }
}

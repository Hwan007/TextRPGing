using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Model;

namespace TextRPGing.Utils
{
    public class IOSteream
    {
        private const string FILE_PATH = "../Data/PlayerData.json";

        public void SavePlayerJsonFile()
        {
            string json = JsonConvert.SerializeObject(Character.Player);
            File.WriteAllText(FILE_PATH, json);
        }

        public Character LoadPlayerJsonFile()
        {
            Character player = null;
            if (File.Exists(FILE_PATH))
            {
                string json = File.ReadAllText(FILE_PATH);
                player = JsonConvert.DeserializeObject<Character>(json);
            }

            return player;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace libs
{
    public class Level
    {
        public Map Map { get; set; }
        public GameObject[] GameObjects { get; set; }
    }

    public class LevelLoader
    {
         public static Level LoadLevel(string levelFilePath)
        {
            // Read JSON data from file
            string jsonData = File.ReadAllText(levelFilePath);

            // Deserialize JSON data into Level object
            Level level = JsonConvert.DeserializeObject<Level>(jsonData);

            return level;
        }
    }
}

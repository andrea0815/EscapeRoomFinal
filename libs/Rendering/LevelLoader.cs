using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace libs
{
    public class Level
    {
        public Map Map { get; set; } = new Map();
        public GameObject[] GameObjects { get; set; } = Array.Empty<GameObject>();
    }

    public class LevelLoader
    {
        public static Level LoadLevel(string levelFilePath)
        {
            // Read JSON data from file
            string jsonData = File.ReadAllText(levelFilePath);

            // Deserialize JSON data into Level object
            Level level = JsonConvert.DeserializeObject<Level>(jsonData) ?? new Level();

            return level;
        }
    }
}

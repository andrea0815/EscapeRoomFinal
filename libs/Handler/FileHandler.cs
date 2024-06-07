﻿﻿using Newtonsoft.Json;

namespace libs
{
    public static class FileHandler
    {
        private static string? filePath;
        private static readonly string envVar = "GAME_SETUP_PATH";

        static FileHandler()
        {
            Initialize();
        }

        private static void Initialize()
        {
            string? envValue = Environment.GetEnvironmentVariable(envVar);
            if (!string.IsNullOrEmpty(envValue))
            {
                filePath = envValue;
                // Console.WriteLine($"Level file path set from environment variable: {filePath}");
            }
            else
            {
                // Default level file path if environment variable is not set
                filePath = "libs/levels/level00.json"; // Updated path
            }
        }

        public static void SetLevelPath(string levelPath)
        {
            filePath = levelPath;
        }

        public static dynamic ReadJson()
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new InvalidOperationException("JSON file path not provided in environment variable");
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                dynamic? jsonData = JsonConvert.DeserializeObject(jsonContent);
                if (jsonData == null)
                {
                    throw new InvalidOperationException("Deserialized JSON data is null");
                }
                return jsonData;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"JSON file not found at path: {filePath}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading JSON file: {ex.Message}");
            }
        }

        public static T ReadJson<T>()
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new InvalidOperationException("JSON file path not provided in environment variable");
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                T? jsonData = JsonConvert.DeserializeObject<T>(jsonContent);
                if (jsonData == null)
                {
                    throw new InvalidOperationException("Deserialized JSON data is null");
                }
                return jsonData;
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"JSON file not found at path: {filePath}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading JSON file: {ex.Message}");
            }
        }
    }
}
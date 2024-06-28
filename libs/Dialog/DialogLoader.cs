using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace libs
{
    public static class DialogLoader
    {
        public static Dictionary<string, string> LoadDialogs(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            else
            {
                throw new FileNotFoundException($"The dialog file {filePath} does not exist.");
            }
        }

        public static string GetDialogFilePath(string relativePath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");
            string combinedPath = Path.Combine(currentDirectory, relativePath);
            Console.WriteLine($"Combined Path: {combinedPath}");
            return combinedPath;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace libs
{
    public static class DialogLoader
    {
        public static Dictionary<string, string>? LoadDialogs(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            else
            {
                return null;
            }
        }

        public static string GetDialogFilePath(string relativePath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string combinedPath = Path.Combine(currentDirectory, relativePath);
            return combinedPath;
        }
    }
}

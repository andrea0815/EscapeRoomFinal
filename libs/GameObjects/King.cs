using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Linq.Expressions;

namespace libs
{
    public class King : GameObject
    {
        public King() : base()
        {
            Type = GameObjectType.King;
            CharRepresentation = '♔';
            Color = ConsoleColor.Red;
            this.Collidable = true;

            var dialog = LoadDialogFromJson("libs/config/dialog.json");
        }

        private static Dialog LoadDialogFromJson(string filePath)
        {
            // Check if the file exists before attempting to read
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The dialog file was not found: {filePath}");
            }
            try
            {
                // JSON deserialization and processing...
                string jsonContent = File.ReadAllText(filePath);
                var dialogSystem = JsonConvert.DeserializeObject<DialogSystem>(jsonContent);
                var nodesDictionary = dialogSystem?.Nodes.ToDictionary(n => n.Id, n => new DialogNode(n.Text)) ?? throw new Exception("Invalid dialog.json");
                foreach (var node in dialogSystem.Nodes)
                {
                    var currentNode = nodesDictionary[node.Id];
                    foreach (var response in node.Responses)
                    {
                        var nextNode = nodesDictionary[response.NextNodeId];
                        currentNode.AddResponse(response.ResponseText, nextNode.Id);
                    }

                }

                return new Dialog(nodesDictionary[dialogSystem.Nodes.First().Id]);
            }
            catch (DirectoryNotFoundException ex)
            {
                // Log the error or notify the user
                Console.WriteLine($"The directory was not found: {ex.Message}");
                throw; // Re-throw the exception if you cannot handle it here
            }
            catch (Exception ex)
            {
                // Handle other exceptions if necessary
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception if you cannot handle it here
            }
        }
    }
}

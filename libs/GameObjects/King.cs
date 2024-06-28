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

            // Adjust the relative path to the JSON file
            string relativeFilePath = "../libs/config/dialog.json";
            string filePath = DialogLoader.GetDialogFilePath(relativeFilePath);

            try
            {
                var dialogs = DialogLoader.LoadDialogs(filePath);

                DialogNode node1 = new DialogNode(dialogs["dialog1"]);
                DialogNode node2 = new DialogNode(dialogs["dialog2"]);
                DialogNode node3 = new DialogNode(dialogs["dialog3"]);
                DialogNode node4 = new DialogNode(dialogs["dialog4"]);
                DialogNode node5 = new DialogNode(dialogs["dialog5"]);
                DialogNode node6 = new DialogNode(dialogs["dialog6"]);

                // Adding responses to nodes
                node1.AddResponse("You: Yes, I need some information.", node2);
                node1.AddResponse("You: No, thanks.", node6);

                node2.AddResponse("You: What do I need to do?", node4);
                node2.AddResponse("You: Why are you here?", node3);
                node2.AddResponse("You: Nevermind.", node6);

                node5.AddResponse("You: What do I need to do?", node4);
                node5.AddResponse("You: Why are you here?", node3);
                node5.AddResponse("You: I had enough information, thank you.", node6);

                node3.AddResponse("You: Oh, how terrible! I will help you!", node5);
                node3.AddResponse("You: Okay, thanks for the information.", node6);

                node4.AddResponse("You: I have some more questions.", node5);
                node4.AddResponse("You: Thanks you for the information!", node6);

                dialogNodes.Add(node1);
                dialogNodes.Add(node2);
                dialogNodes.Add(node3);
                dialogNodes.Add(node4);
                dialogNodes.Add(node5);
                dialogNodes.Add(node6);

                dialog = new Dialog(node1);
            }
            catch (Exception ex)
            {
                Color = ConsoleColor.White;
            }

        }
    }
}

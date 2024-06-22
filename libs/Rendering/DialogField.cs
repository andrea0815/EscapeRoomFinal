using System;

namespace libs
{
    public class DialogBox
    {
        private int width;
        private int height;

        public DialogBox()
        {
            this.width = 50; // Increased width to accommodate options
            this.height = 6; // Increased height to accommodate multiple lines
        }

        // Method to display the dialog box with the provided message and options
        public void Show(string message, string[] options = null)
        {
            string[] formattedMessage = FormatMessage(message);
            var contentLines = new System.Collections.Generic.List<string>(formattedMessage);

            if (options != null)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    contentLines.Add($"{i + 1}. {options[i]}");
                }
            }

            // Adjust height based on the content length
            height = Math.Max(this.height, contentLines.Count + 4);

            // Draw the top border
            Console.WriteLine("+" + new string('-', width - 2) + "+");

            // Print each line within the box
            for (int i = 0; i < height - 2; i++)
            {
                if (i < contentLines.Count)
                {
                    string textLine = contentLines[i];
                    Console.WriteLine("| " + textLine + new string(' ', width - 3 - textLine.Length) + "|");
                }
                else
                {
                    Console.WriteLine("|" + new string(' ', width - 2) + "|");
                }
            }

            // Draw the bottom border
            Console.WriteLine("+" + new string('-', width - 2) + "+");
        }

        // Method to wrap and format the message to fit within the dialog box
        private string[] FormatMessage(string message)
        {
            string[] words = message.Split(' ');
            string line = "";
            var lines = new System.Collections.Generic.List<string>();

            foreach (var word in words)
            {
                if (line.Length + word.Length + 2 < width - 2) // +2 for padding // -2 for the box borders
                {
                    line += (line.Length == 0 ? "" : " ") + word;
                }
                else
                {
                    lines.Add(line);
                    line = word; // Start a new line with the current word
                }
            }

            if (!string.IsNullOrEmpty(line))
            {
                lines.Add(line);
            }

            return lines.ToArray();
        }

        // Method to get user input inside the dialog box
        public int GetInput(int optionsCount)
        {
            int choice = 0;
            while (true)
            {
                Console.SetCursorPosition(2, height + 1);
                Console.Write("Choose an option: ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= optionsCount)
                {
                    break;
                }
                Console.SetCursorPosition(2, height + 2);
                Console.WriteLine("Invalid choice, please try again.");
            }
            return choice;
        }
    }
}

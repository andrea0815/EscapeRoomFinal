using System;
namespace libs;


public class DialogBox
{
    private int width;
    private int height;

    public DialogBox()
    {
        this.width = 35;
        this.height = 8;
    }

    // Method to display the dialog box with the provided message
    public void Show(string message)
    {
        // Prepare the message to be displayed within the box
        string[] formattedMessage = FormatMessage(message);

        // Draw the top border
        Console.WriteLine("+" + new string('-', width - 2) + "+");

        // Print each line within the box
        for (int i = 0; i < height - 2; i++)
        {
            if (i < formattedMessage.Length)
            {
                string textLine = formattedMessage[i];
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
}
using System.Data.Common;
using System.Net;

namespace libs;

public class DialogNode
{
    public string Id { get; set; } // Add the 'Id' property
    public string Text { get; set; }
    public List<Response> Responses { get; set; }

    public DialogNode(string text)
    {
        Text = text;
        Responses = new List<Response>();
        Id = string.Empty;
    }

    public void AddResponse(string responseText, string nextNodeId)
    {
        Responses.Add(new Response(responseText, nextNodeId));
    }
}
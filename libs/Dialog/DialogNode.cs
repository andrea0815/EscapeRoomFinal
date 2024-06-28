using System.Data.Common;
using System.Net;

namespace libs;

public class DialogNode(string text, string id = "", List<Response>? responses = null)
{

    public string DialogID { get; set; } = id;
    public string Text { get; set; } = text;
    public List<Response> Responses = responses ?? new List<Response>();

    public void AddResponse(string responseText, DialogNode nextNode)
    {
        Responses.Add(new Response(responseText, nextNode));
    }

}
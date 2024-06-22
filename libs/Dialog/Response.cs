namespace libs;

public class Response(string responseText, string nextNodeId)
{
    public string ResponseText { get; set; } = responseText;
    public string NextNodeId { get; set; } = nextNodeId;
}
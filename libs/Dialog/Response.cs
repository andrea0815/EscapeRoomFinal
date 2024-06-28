namespace libs;

public class Response(string responseText, DialogNode nextNode)
{
    public string ResponseText { get; set; } = responseText;
    public DialogNode NextNode { get; set; } = nextNode;
}
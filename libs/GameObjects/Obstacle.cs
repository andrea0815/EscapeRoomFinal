namespace libs;

public class Obstacle : GameObject {
    public Obstacle () : base() {
        this.Type = GameObjectType.Obstacle;
        this.CharRepresentation = '█';
        this.Color = ConsoleColor.Cyan;
        this.Collidable = true;


        //TODO Import and add those from JSON
        DialogNode node1 = new DialogNode("King: Hello fellow, I need your help to escape this prison. Do you need more information?");
        DialogNode node2 = new DialogNode("King: Sure, what do you want to know?");
        DialogNode node3 = new DialogNode("King: I was imprisoned here for a crime I didn't commit. I need to escape to prove my innocence.");
        DialogNode node4 = new DialogNode("King: You have to find the key to open the door. They are both yellow, so you can't miss them.");
        DialogNode node5 = new DialogNode("King: Do you need some more information?");
        DialogNode node6 = new DialogNode("King: Good luck, fellow!");

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
}
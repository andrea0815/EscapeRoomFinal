namespace libs;

public class Obstacle : GameObject {
    public Obstacle () : base() {
        this.Type = GameObjectType.Obstacle;
        this.CharRepresentation = '█';
        this.Color = ConsoleColor.DarkBlue;
        this.Collidable = true;


        //TODO Import and add those from JSON
        DialogNode node1 = new DialogNode("Hello, how can I help you?");
        DialogNode node2 = new DialogNode("Sure, what information do you need?");
        DialogNode node3 = new DialogNode("Sorry, I can't help with that.");
        DialogNode node4 = new DialogNode("Here is the information you requested.");
        DialogNode node5 = new DialogNode("Goodbye!");

        // Adding responses to nodes
        node1.AddResponse("I need some information.", node2);
        node1.AddResponse("Nothing, thanks.", node5);

        node2.AddResponse("Tell me more.", node4);
        node2.AddResponse("Nevermind.", node3);

        node3.AddResponse("Okay, goodbye.", node5);
        node4.AddResponse("Thanks!", node5);

        dialogNodes.Add(node1);
        dialogNodes.Add(node2);
        dialogNodes.Add(node3);
        dialogNodes.Add(node4);
        dialogNodes.Add(node5);

        dialog = new Dialog(node1);
    }
}
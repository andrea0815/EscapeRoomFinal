namespace libs
{
    public class Dialog(DialogNode startingNode)
    {
        private DialogNode _currentNode = startingNode;
        private DialogNode _startingNode = startingNode;
        private DialogNode _endNode = new("There is nothing left to say...");
        private DialogBox _dialogBox = new();

        public void Start()
        {
            while (_currentNode != null)
            {
                // Clear the previous content
                Console.Clear();

                // Show the current dialog text and response options
                var options = _currentNode.Responses.Select(r => r.ResponseText).ToArray();
                _dialogBox.Show(_currentNode.Text, options);

                if (_currentNode.Responses.Count == 0)
                    break;

                int choice = _dialogBox.GetInput(_currentNode.Responses.Count);

                _currentNode = new DialogNode(_currentNode.Responses[choice - 1].NextNodeId);
            }

            _currentNode = _endNode;

            // Clear the previous content
            Console.Clear();

            // Show the end of dialog message
            _dialogBox.Show("End of dialog.");
            Thread.Sleep(1000);
        }
    }
}

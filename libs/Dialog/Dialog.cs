namespace libs
{
    public class Dialog
    {
        private DialogNode _currentNode;
        private DialogNode _startingNode;
        private DialogNode _endNode;
        private DialogBox _dialogBox;

        public Dialog(DialogNode startingNode)
        {
            _startingNode = startingNode;
            _currentNode = startingNode;
            _endNode = new DialogNode("There is nothing left to say...");
            _dialogBox = new DialogBox();
        }

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

                _currentNode = _currentNode.Responses[choice - 1].NextNode;
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

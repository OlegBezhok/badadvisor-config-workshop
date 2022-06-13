namespace BadAdvisor.Mvc.Data
{
    public class MessagesRepository : IMessagesRepository
    {
        private const string TableName = "commitmessage";
        private const string PartitionKey = "fun";

        private readonly string[] _messages = new string[]
        {
            "This is not a commit",
            "This commit is a lie",
            "It fucking compiles",
            "Too tired to write descriptive message",
            "Removing unnecessary shit",
        };

        public int GetTotalCount()
        {
            return _messages.Length;
        }

        public async Task<Message> Get(int id)
        {
            var message = new Message()
            {
                Text = _messages[id]
            };
            return await Task.FromResult(message);
        }
    }

    public interface IMessagesRepository
    {
        int GetTotalCount();

        Task<Message> Get(int id);
    }
}
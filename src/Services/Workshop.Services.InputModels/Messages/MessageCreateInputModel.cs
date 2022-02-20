namespace Workshop.Services.InputModels.Messages
{
    public class MessageCreateInputModel
    {
        public string Content { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }
    }
}

namespace Workshop.Web.ViewModels.Chat
{
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class MessageViewModel : IMapFrom<Message>
    {
        private string senderAvatarUrl;

        public string SenderId { get; set; }

        public string SenderUserName { get; set; }

        public string SenderAvatarUrl
        {
            get
            {
                return this.senderAvatarUrl;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.senderAvatarUrl = @"/anonymous-avatar.jpg";
                }
                else
                {
                    this.senderAvatarUrl = value;
                }
            }
        }

        public string Content { get; set; }

        public string CreatedOn { get; set; }
    }
}

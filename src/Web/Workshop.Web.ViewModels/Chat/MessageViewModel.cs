namespace Workshop.Web.ViewModels.Chat
{
    using System;

    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    using static Workshop.Common.GlobalConstants;

    public class MessageViewModel : IMapFrom<Message>
    {
        private string senderAvatarUrl;
        private string createdOn;

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

        public string CreatedOn
        {
            get
            {
                return this.createdOn;
            }

            set
            {
                this.createdOn = DateTime.Parse(value).ToString(DateTimeFormat);
            }
        }
    }
}

namespace Workshop.Web.ViewModels.Notifications
{
    using System;

    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        private string senderAvatarUrl;

        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsSeen { get; set; }

        public DateTime CreatedOn { get; set; }

        public string SenderId { get; set; }

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

        public string SenderUserName { get; set; }

        public string Link { get; set; }
    }
}

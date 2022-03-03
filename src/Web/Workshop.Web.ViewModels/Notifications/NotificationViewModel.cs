namespace Workshop.Web.ViewModels.Notifications
{
    using System;

    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    using static Workshop.Common.GlobalConstants;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        private string senderAvatarUrl;
        private string createdOn;

        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsSeen { get; set; }

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

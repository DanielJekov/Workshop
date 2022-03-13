namespace Workshop.Services.Data.NotificationsUsersStatusCollection
{
    using System;

    public class NotificationHubUsersStatusModel
    {
        public string UserId { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastActiveOn { get; set; }
        = DateTime.UtcNow;
    }
}

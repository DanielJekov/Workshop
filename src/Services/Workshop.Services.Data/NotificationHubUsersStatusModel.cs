namespace Workshop.Services.Data
{
    using System;

    public class NotificationHubUsersStatusModel
    {
        public string UserName { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastActiveOn { get; set; }
        = DateTime.UtcNow;
    }
}

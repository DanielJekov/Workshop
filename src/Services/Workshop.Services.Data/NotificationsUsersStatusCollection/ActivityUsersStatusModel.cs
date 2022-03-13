namespace Workshop.Services.Data.ActivityUsersStatusCollection
{
    using System;

    public class ActivityUsersStatusModel
    {
        public string UserId { get; set; }

        public bool IsActive { get; set; }

        public DateTime LastActiveOn { get; set; }
        = DateTime.UtcNow;
    }
}

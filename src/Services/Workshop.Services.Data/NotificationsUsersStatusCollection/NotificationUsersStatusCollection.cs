namespace Workshop.Services.Data.NotificationsUsersStatusCollection
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class NotificationUsersStatusCollection : INotificationUsersStatusCollection
    {
        private List<NotificationHubUsersStatusModel> users = new List<NotificationHubUsersStatusModel>();

        public ReadOnlyCollection<NotificationHubUsersStatusModel> UsersCollection
            => new ReadOnlyCollection<NotificationHubUsersStatusModel>(this.users);

        public void Add(string userId)
        {
            this.users.Add(new NotificationHubUsersStatusModel { UserId = userId });
            this.Active(userId);
        }

        public void Remove(string userId)
        {
            var userToRemove = this.users.FirstOrDefault(x => x.UserId == userId);
            this.users.Remove(userToRemove);
        }

        public void Active(string userId)
        {
            var user = this.users.FirstOrDefault(x => x.UserId == userId);
            user.IsActive = true;
            user.LastActiveOn = DateTime.UtcNow;
        }

        public void NonActive(string userId)
        {
            var user = this.users.FirstOrDefault(x => x.UserId == userId);
            user.IsActive = false;
            user.LastActiveOn = DateTime.UtcNow;
        }

        public bool IsActive(string userId)
        => this.users.FirstOrDefault(x => x.UserId == userId).IsActive;

        public bool IsInCollection(string userId)
        => this.users.FirstOrDefault(x => x.UserId == userId) != null ? true : false;
    }
}

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

        public void Add(string userName)
        {
            this.users.Add(new NotificationHubUsersStatusModel { UserName = userName });
            this.Active(userName);
        }

        public void Remove(string userName)
        {
            var userToRemove = this.users.FirstOrDefault(x => x.UserName == userName);
            this.users.Remove(userToRemove);
        }

        public void Active(string userName)
        {
            var user = this.users.FirstOrDefault(x => x.UserName == userName);
            user.IsActive = true;
            user.LastActiveOn = DateTime.UtcNow;
        }

        public void NonActive(string userName)
        {
            var user = this.users.FirstOrDefault(x => x.UserName == userName);
            user.IsActive = false;
            user.LastActiveOn = DateTime.UtcNow;
        }

        public bool IsActive(string userName)
        => this.users.FirstOrDefault(x => x.UserName == userName).IsActive;

        public bool IsInCollection(string userName)
        => this.users.FirstOrDefault(x => x.UserName == userName) != null ? true : false;
    }
}

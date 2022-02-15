namespace Workshop.Services.Data
{
    using System.Collections.ObjectModel;

    public interface INotificationUsersStatusCollection
    {
        public ReadOnlyCollection<NotificationHubUsersStatusModel> UsersCollection { get; }

        public void Add(string userName);

        public void Remove(string userName);

        public void Active(string userName);

        public void NonActive(string userName);

        public bool IsActive(string userName);

        public bool IsInCollection(string userName);
    }
}

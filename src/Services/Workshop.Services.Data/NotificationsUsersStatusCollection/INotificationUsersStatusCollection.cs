namespace Workshop.Services.Data.NotificationsUsersStatusCollection
{
    using System.Collections.ObjectModel;

    public interface INotificationUsersStatusCollection
    {
        ReadOnlyCollection<NotificationHubUsersStatusModel> UsersCollection { get; }

        void Add(string userName);

        void Remove(string userName);

        void Active(string userName);

        void NonActive(string userName);

        bool IsActive(string userName);

        bool IsInCollection(string userName);
    }
}

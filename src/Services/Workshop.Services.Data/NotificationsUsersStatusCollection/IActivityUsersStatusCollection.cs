namespace Workshop.Services.Data.ActivityUsersStatusCollection
{
    using System.Collections.ObjectModel;

    public interface IActivityUsersStatusCollection
    {
        ReadOnlyCollection<ActivityUsersStatusModel> UsersCollection { get; }

        void Add(string userName);

        void Remove(string userName);

        void Active(string userName);

        void NonActive(string userName);

        bool IsActive(string userName);

        bool IsInCollection(string userName);
    }
}

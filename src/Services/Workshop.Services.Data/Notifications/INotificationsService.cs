namespace Workshop.Services.Data.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Workshop.Services.InputModels.Notifications;

    public interface INotificationsService
    {
        public Task CreateAsync(NotificationCreateInputModel input);

        public IEnumerable<T> GetAllUnseen<T>(string userId);

        public IEnumerable<T> GetAllSeen<T>(string userId);

        public Task UpdateSeenStatusToAllAsync(string userId);

        public int GetCountOfUnseen(string userId);
    }
}

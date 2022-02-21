namespace Workshop.Services.Data.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Workshop.Services.InputModels.Notifications;

    public interface INotificationsService
    {
        Task CreateAsync(NotificationCreateInputModel input);

        IEnumerable<T> GetAllUnseen<T>(string userId);

        IEnumerable<T> GetAllSeen<T>(string userId);

        Task UpdateSeenStatusToAllAsync(string userId);

        int GetCountOfUnseen(string userId);
    }
}

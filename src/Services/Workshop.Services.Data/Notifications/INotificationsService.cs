namespace Workshop.Services.Data.Notifications
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Workshop.Services.InputDtos.Notifications;

    public interface INotificationsService
    {
        Task CreateAsync(NotificationCreateInputDto input);

        IEnumerable<T> GetAllUnseen<T>(string userId);

        IEnumerable<T> GetAllSeen<T>(string userId);

        Task UpdateSeenStatusToAllAsync(string userId);

        int GetCountOfUnseen(string userId);
    }
}

namespace Workshop.Services.Data.Notifications
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Services.InputDtos.Notifications;
    using Workshop.Services.Mapping;

    public class NotificationsService : INotificationsService
    {
        private readonly IDeletableEntityRepository<Notification> notificationRepository;

        public NotificationsService(IDeletableEntityRepository<Notification> notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task CreateAsync(NotificationCreateInputDto input)
        {
            var notification = new Notification()
            {
                SenderId = input.SenderId,
                ReceiverId = input.ReceiverId,
                Description = input.Description,
            };

            await this.notificationRepository.AddAsync(notification);
            await this.notificationRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAllUnseen<T>(string userId)
        {
            return this.notificationRepository
                                    .All()
                                    .Where(n => n.Receiver.Id == userId && n.IsSeen == false)
                                    .OrderByDescending(n => n.CreatedOn)
                                    .To<T>()
                                    .ToList();
        }

        public IEnumerable<T> GetAllSeen<T>(string userId)
        {
            return this.notificationRepository
                                    .All()
                                    .Where(n => n.Receiver.Id == userId && n.IsSeen == true)
                                    .OrderByDescending(n => n.CreatedOn)
                                    .To<T>()
                                    .ToList();
        }

        public async Task UpdateSeenStatusToAllAsync(string userId)
        {
            var notifications = this.notificationRepository
                              .All()
                              .Where(n => n.Receiver.Id == userId && n.IsSeen == false)
                              .ToList();

            foreach (var notification in notifications)
            {
                notification.IsSeen = true;
            }

            await this.notificationRepository.SaveChangesAsync();
        }

        public int GetCountOfUnseen(string userId)
        {
            return this.notificationRepository
                       .All()
                       .Where(n => n.Receiver.Id == userId && n.IsSeen == false)
                       .Count();
        }
    }
}

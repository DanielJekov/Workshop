namespace Workshop.Services.Data.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Services.InputModels.Messages;
    using Workshop.Services.Mapping;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public MessagesService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task CreateAsync(MessageCreateInputModel input)
        {
            var message = new Message()
            {
                SenderId = input.SenderId,
                ReceiverId = input.ReceiverId,
                Content = input.Content,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(string currentUserId, string otherUserId)
        {
            return this.messagesRepository
                       .AllAsNoTracking()
                       .Where(x => (x.Sender.Id == currentUserId && x.Receiver.Id == otherUserId) ||
                                    (x.Sender.Id == otherUserId && x.Receiver.Id == currentUserId))
                       .To<T>()
                       .ToList();
        }
    }
}

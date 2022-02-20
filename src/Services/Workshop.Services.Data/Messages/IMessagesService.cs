namespace Workshop.Services.Data.Messages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Workshop.Services.InputModels.Messages;

    public interface IMessagesService
    {
        public Task CreateAsync(MessageCreateInputModel input);

        public IEnumerable<T> GetAll<T>(string currentUserId, string otherUserId);
    }
}

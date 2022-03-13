namespace Workshop.Services.Data.Messages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Workshop.Services.InputDtos.Messages;

    public interface IMessagesService
    {
        Task CreateAsync(MessageCreateInputDto input);

        IEnumerable<T> GetAll<T>(string currentUserId, string otherUserId);
    }
}

namespace Workshop.Services.Data.Topics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Workshop.Services.InputDtos.Topics;

    public interface ITopicsService
    {
        Task CreateAsync(TopicInputDto input);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAll<T>(int courseId);

        public Task ChangeNoteAsync(int topicId, string noteValue);
    }
}

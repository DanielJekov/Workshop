namespace Workshop.Services.Data.Topics
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Workshop.Services.InputModels.Topics;

    public interface ITopicsService
    {
        Task CreateAsync(TopicInputModel input);

        Task DeleteAsync(int id);

        IEnumerable<T> GetAll<T>(int courseId);
    }
}

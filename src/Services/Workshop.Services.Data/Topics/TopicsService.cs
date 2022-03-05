namespace Workshop.Services.Data.Topics
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Services.InputModels.Topics;
    using Workshop.Services.Mapping;

    public class TopicsService : ITopicsService
    {
        private readonly IDeletableEntityRepository<Topic> topicsRepository;

        public TopicsService(IDeletableEntityRepository<Topic> topicsRepository)
        {
            this.topicsRepository = topicsRepository;
        }

        public async Task CreateAsync(TopicInputModel input)
        {
            var topic = new Topic()
            {
                CourseId = input.CourseId,
                Name = input.Name,
                Note = input.Note,
                YoutubeLink = input.YoutubeLink,
            };

            await this.topicsRepository.AddAsync(topic);
            await this.topicsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var topic = this.topicsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.topicsRepository.Delete(topic);
            await this.topicsRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int courseId)
        {
           return this.topicsRepository
                      .AllAsNoTracking()
                      .Where(x => x.Course.Id == courseId)
                      .To<T>()
                      .ToList();
        }

        public async Task ChangeNoteAsync(int topicId, string noteValue)
        {
            this.topicsRepository
                .All()
                .Where(x => x.Id == topicId)
                .FirstOrDefault()
                .Note = noteValue;
            await this.topicsRepository.SaveChangesAsync();
        }
    }
}

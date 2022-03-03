namespace Workshop.Services.Data.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class SearchService : ISearchService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Course> courseRepository;
        private readonly IDeletableEntityRepository<Topic> topicRepository;

        public SearchService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<Course> courseRepository,
            IDeletableEntityRepository<Topic> topicRepository)
        {
            this.userRepository = userRepository;
            this.courseRepository = courseRepository;
            this.topicRepository = topicRepository;
        }

        public ICollection<T> Courses<T>(string searchWord)
        {
            var result = this.courseRepository
                   .AllAsNoTracking()
                   .Where(x => x.Name.Contains(searchWord))
                   .To<T>()
                   .ToList();

            return result;
        }

        public ICollection<T> Topics<T>(string searchWord)
        {
            var result = this.topicRepository
                      .AllAsNoTracking()
                      .Where(x => x.Name.Contains(searchWord))
                      .To<T>()
                      .ToList();

            return result;
        }

        public ICollection<T> Users<T>(string searchWord)
        {
            var result = this.userRepository
                       .AllAsNoTracking()
                       .Where(x => x.UserName.Contains(searchWord))
                       .To<T>()
                       .ToList();

            return result;
        }
    }
}

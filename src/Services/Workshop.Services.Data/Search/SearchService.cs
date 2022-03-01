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

        public SearchService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public ICollection<T> Users<T>(string searchWord)
        {
            var result = this.userRepository
                       .AllAsNoTracking()
                       .Where(x => x.UserName.Contains(searchWord) ||
                                   x.Email.Contains(searchWord))
                       .To<T>()
                       .ToList();

            return result;
        }
    }
}

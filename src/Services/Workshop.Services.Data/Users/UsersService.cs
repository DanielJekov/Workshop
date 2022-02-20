namespace Workshop.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Linq;

    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.usersRepository.AllAsNoTracking().To<T>().ToList();
        }
    }
}

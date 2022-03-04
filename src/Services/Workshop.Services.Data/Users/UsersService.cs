namespace Workshop.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task ChangeAvatarAsync(string userId, string avatarUrl, string avatarHash)
        {
            var user = this.usersRepository.All().Where(x => x.Id == userId).FirstOrDefault();
            user.AvatarUrl = avatarUrl;
            user.AvatarHash = avatarHash;
            await this.usersRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.usersRepository.AllAsNoTracking().To<T>().ToList();
        }

        public string GetAvatarHash(string userId)
        {
            return this.usersRepository
                       .AllAsNoTracking()
                       .Where(x => x.Id == userId)
                       .Select(x => x.AvatarHash)
                       .FirstOrDefault();
        }
    }
}

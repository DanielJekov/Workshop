namespace Workshop.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUsersService
    {
        IEnumerable<T> GetAll<T>();

        Task ChangeAvatarAsync(string userId, string avatarUrl, string avatarHash);

        string GetAvatarHash(string userId);
    }
}

namespace Workshop.Services.Data.Roles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRolesService
    {
        IEnumerable<T> GetAll<T>();

        Task AddAsync(string name);

        Task RemoveAsync(string id);

        Task AddUserToRoleAsync(string roleId, string userId);

        Task RemoveUserFromRoleAsync(string roleId, string userId);

        IEnumerable<T> UsersWhoAreNotInGivenRole<T>(string roleId);

        IEnumerable<T> GetUsersFromGivenRole<T>(string roleId);
    }
}

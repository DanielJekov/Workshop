namespace Workshop.Services.Data.Roles
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRolesService
    {
        IEnumerable<T> GetAll<T>();

        Task AddAsync(string name);

        Task RemoveAsync(string id);

        IEnumerable<T> GetUsersFromGivenRole<T>(string roleId);

        Task AddUserToRole(string roleId, string userId);

        Task RemoveUserFromRole(string roleId, string userId);

        IEnumerable<T> UsersWhoAreNotInGivenRole<T>(string roleId);
    }
}

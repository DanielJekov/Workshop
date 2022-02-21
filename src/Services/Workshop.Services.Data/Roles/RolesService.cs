namespace Workshop.Services.Data.Roles
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> rolesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RolesService(
            IDeletableEntityRepository<ApplicationRole> rolesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.rolesRepository = rolesRepository;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.usersRepository = usersRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.rolesRepository.AllAsNoTracking().To<T>().ToList();
        }

        public async Task AddAsync(string name)
        {
            await this.rolesRepository.AddAsync(new ApplicationRole { Name = name });
            await this.rolesRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(string id)
        {
            var role = this.rolesRepository.All().Where(r => r.Id == id).FirstOrDefault();
            this.rolesRepository.Delete(role);
            await this.rolesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetUsersFromGivenRole<T>(string roleId)
        {
            return this.userManager.Users
                                   .Where(x => x.Roles.Any(x => x.RoleId == roleId))
                                   .To<T>()
                                   .ToList();
        }

        public async Task AddUserToRole(string roleId, string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var roleName = await this.roleManager.GetRoleNameAsync(await this.roleManager.FindByIdAsync(roleId));
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        public async Task RemoveUserFromRole(string roleId, string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var roleName = await this.roleManager.GetRoleNameAsync(await this.roleManager.FindByIdAsync(roleId));
            await this.userManager.RemoveFromRoleAsync(user, roleName);
        }

        public IEnumerable<T> UsersWhoAreNotInGivenRole<T>(string roleId)
        {
            return this.usersRepository
                       .AllAsNoTracking()
                       .Where(x => x.Roles.All(r => r.RoleId != roleId))
                       .To<T>()
                       .ToList();
        }
    }
}

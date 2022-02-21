namespace Workshop.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Workshop.Services.Data.Roles;
    using Workshop.Web.ViewModels.Roles;

    public class RolesController : AdministrationController
    {
        private readonly IRolesService rolesService;

        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        public IActionResult All()
        {
            var viewModel = this.rolesService.GetAll<RoleViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            await this.rolesService.AddAsync(name);

            return this.Redirect($"/{nameof(AdministrationController).Replace("Controller", string.Empty)}" +
                                 $"/{nameof(RolesController).Replace("Controller", string.Empty)}" +
                                 $"/{nameof(this.All)}");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string id)
        {
            await this.rolesService.RemoveAsync(id);

            return this.Redirect($"/{nameof(AdministrationController).Replace("Controller", string.Empty)}" +
                                $"/{nameof(RolesController).Replace("Controller", string.Empty)}" +
                                $"/{nameof(this.All)}");
        }
    }
}

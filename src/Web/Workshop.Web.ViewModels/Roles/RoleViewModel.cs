namespace Workshop.Web.ViewModels.Roles
{
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class RoleViewModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

namespace Workshop.Web.ViewModels.Roles
{
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }
    }
}

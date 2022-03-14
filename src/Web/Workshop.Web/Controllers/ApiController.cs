namespace Workshop.Web.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using Workshop.Common;
    using Workshop.Services.Data.Notifications;
    using Workshop.Services.Data.Roles;
    using Workshop.Services.Data.Search;
    using Workshop.Services.Data.Topics;
    using Workshop.Web.ViewModels.Courses;
    using Workshop.Web.ViewModels.Notifications;
    using Workshop.Web.ViewModels.Roles;
    using Workshop.Web.ViewModels.Search;
    using Workshop.Web.ViewModels.Topics;

    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly INotificationsService notificationsService;
        private readonly IRolesService roleService;
        private readonly ISearchService searchService;
        private readonly ITopicsService topicsService;

        public ApiController(
            IConfiguration configuration,
            INotificationsService notificationsService,
            ISearchService searchService,
            IRolesService roleService,
            ITopicsService topicsService)
        {
            this.configuration = configuration;
            this.notificationsService = notificationsService;
            this.roleService = roleService;
            this.searchService = searchService;
            this.topicsService = topicsService;
        }

        [Route("weather")]
        public async Task<object> Weather(string lat, string lon)
        {
            var apiKey = this.configuration.GetValue<string>("WeatherApiKey");

            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            string jsonResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonResponse = await reader.ReadToEndAsync();
            }

            var jsonObject = JsonSerializer.Deserialize<object>(jsonResponse);

            return jsonObject;
        }

        [Route("github-repos")]
        public async Task<object> Github()
        {
            var url = $"https://api.github.com/users/danieljekov/repos";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.UserAgent = "myTestApp";

            string jsonResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                jsonResponse = await reader.ReadToEndAsync();
            }

            var jsonObject = JsonSerializer.Deserialize<object>(jsonResponse);

            return jsonObject;
        }

        [Authorize]
        [Route("notifications")]
        public async Task<ICollection<NotificationViewModel>> Notifications()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var notifications = this.notificationsService.GetAllUnseen<NotificationViewModel>(userId).ToList();
            await this.notificationsService.UpdateSeenStatusToAllAsync(userId);

            return notifications;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Route("RemoveRole/{id}")]
        public async Task RemoveRole(string id)
        {
            await this.roleService.RemoveAsync(id);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Route("role-users/{id}")]
        public IEnumerable<UserViewModel> GetUsersFromGivenRole(string id)
        {
            var usersFromGivenRole = this.roleService.GetUsersFromGivenRole<UserViewModel>(id).ToList();

            return usersFromGivenRole;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost("remove-user-from-given-role")]
        public async Task RemoveUserFromRole([FromForm] string roleId, [FromForm] string userId)
        {
            await this.roleService.RemoveUserFromRoleAsync(roleId, userId);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [Route("users-who-are-not-in-given-role/{id}")]
        public ICollection<UserViewModel> UsersWhoAreNotInGivenRole(string id)
        {
            var result = this.roleService.UsersWhoAreNotInGivenRole<UserViewModel>(id).ToList();
            return result;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost("add-user-to-role")]
        public async Task AddUserToRole([FromForm] string roleId, [FromForm] string userId)
        {
            await this.roleService.AddUserToRoleAsync(roleId, userId);
        }

        [Route("search")]
        [Authorize]
        public SearchViewModel Search([FromQuery] string searchText)
        {
            ICollection<CourseViewModel> courses = null;
            ICollection<TopicViewModel> topics = null;

            if (this.User.IsInRole("Learning"))
            {
                courses = this.searchService.Courses<CourseViewModel>(searchText);
                topics = this.searchService.Topics<TopicViewModel>(searchText);
            }

            var users = this.searchService.Users<Workshop.Web.ViewModels.Users.UserViewModel>(searchText);

            var result = new SearchViewModel
            {
                Users = users,
                Courses = courses,
                Topics = topics,
            };

            return result;
        }

        [HttpPost("change-note")]
        [Authorize(Roles = "Learning")]
        public async Task ChangeNote([FromForm] int topicId, [FromForm] string noteValue)
        {
            await this.topicsService.ChangeNoteAsync(topicId, noteValue);
        }
    }
}

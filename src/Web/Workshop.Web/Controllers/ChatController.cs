namespace Workshop.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Workshop.Data;
    using Workshop.Services.Data;
    using Workshop.Web.ViewModels.Chat;

    [Authorize]
    public class ChatController : BaseController
    {
        private ApplicationDbContext db;
        private INotificationUsersStatusCollection usersStatusCollection;

        public ChatController(ApplicationDbContext db, INotificationUsersStatusCollection usersStatusCollection)
        {
            this.db = db;
            this.usersStatusCollection = usersStatusCollection;
        }

        public IActionResult Private(string id)
        {
            var usersModel = this.db.Users
                               .Select(x => new ChatUserViewModel()
                               {
                                   Id = x.Id,
                                   UserName = x.UserName,
                               })
                               .ToList();

            foreach (var user in usersModel)
            {
                var userActivityInfo = this.usersStatusCollection.UsersCollection
                    .FirstOrDefault(u => u.UserName == user.UserName);
                if (userActivityInfo == null)
                {
                    continue;
                }

                user.LastActiveOn = userActivityInfo.LastActiveOn;
                user.IsActive = userActivityInfo.IsActive;
            }

            this.ViewBag.Username = usersModel.FirstOrDefault(x => x.Id == id)?.UserName;

            return this.View(usersModel);
        }

        public IActionResult UsersList()
        {
            var usersModel = this.db.Users
                               .Select(x => new ChatUserViewModel()
                               {
                                   Id = x.Id,
                                   UserName = x.Email,
                               })
                               .ToList();
            foreach (var user in usersModel)
            {
                var userActivityInfo = this.usersStatusCollection.UsersCollection
                    .FirstOrDefault(u => u.UserName == user.UserName);

                if (userActivityInfo == null)
                {
                    continue;
                }

                user.LastActiveOn = userActivityInfo.LastActiveOn;
                user.IsActive = userActivityInfo.IsActive;
            }

            return this.View(usersModel);
        }
    }
}

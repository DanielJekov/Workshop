namespace Workshop.Web.Controllers
{
    using System.Linq;
    using System.Security.Claims;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Workshop.Services.Data.Messages;
    using Workshop.Services.Data.NotificationsUsersStatusCollection;
    using Workshop.Services.Data.Users;
    using Workshop.Web.ViewModels.Chat;
    using Workshop.Web.ViewModels.Users;

    [Authorize]
    public class ChatController : BaseController
    {
        private INotificationUsersStatusCollection usersStatusCollection;
        private IUsersService usersService;
        private IMessagesService messagesService;

        public ChatController(
            IUsersService usersService,
            INotificationUsersStatusCollection usersStatusCollection,
            IMessagesService messagesService)
        {
            this.usersStatusCollection = usersStatusCollection;
            this.usersService = usersService;
            this.messagesService = messagesService;
        }

        public IActionResult Private(string id)
        {
            var currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var usersModel = this.usersService.GetAll<UserViewModel>().ToList();
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

            var messagesModel = this.messagesService.GetAll<MessageViewModel>(currentUserId, id).ToList();
            var viewModel = new ChatWithUserViewModel()
            {
                CurrentUser = usersModel.FirstOrDefault(u => u.Id == currentUserId),
                OtherUser = usersModel.FirstOrDefault(u => u.Id == id),
                Messages = messagesModel,
                Users = usersModel,
            };

            return this.View(viewModel);
        }

        public IActionResult UsersList()
        {
            var usersModel = this.usersService.GetAll<UserViewModel>().ToList();

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

namespace Workshop.Web.Components
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Workshop.Data.Models;
    using Workshop.Services.Data.Notifications;
    using Workshop.Web.ViewModels.Notifications;

    [Authorize]
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly INotificationsService notificationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationsViewComponent(
            INotificationsService notificationsService,
            UserManager<ApplicationUser> userManager)
        {
            this.notificationsService = notificationsService;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = this.userManager.GetUserId(this.Request.HttpContext.User);
            var viewModel = new NotificationsViewComponentViewModel
            {
                UnseenCount = this.notificationsService.GetCountOfUnseen(userId),
                Notifications = this.notificationsService.GetAllSeen<NotificationViewModel>(userId).ToList(),
            };

            return this.View(viewModel);
        }
    }
}

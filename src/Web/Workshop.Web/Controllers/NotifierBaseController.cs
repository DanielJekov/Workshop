namespace Workshop.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;

    using Workshop.Web.Hubs;

    public class NotifierBaseController : Controller
    {
        private IHubContext<NotificationHub> notificationHubContext;

        public NotifierBaseController(IHubContext<NotificationHub> notificationHubContext)
        {
            this.notificationHubContext = notificationHubContext;
        }

        public IHubContext<NotificationHub> NotificationHubContext => this.notificationHubContext;
    }
}

namespace Workshop.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;

    using Workshop.Services.Data;
    using Workshop.Services.Data.Notifications;
    using Workshop.Web.ViewModels;

    public class HomeController : BaseController
    {
        private INotificationsService notificationsService;

        public HomeController(INotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

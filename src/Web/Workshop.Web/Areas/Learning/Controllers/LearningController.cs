namespace Workshop.Web.Areas.Resourses.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Learning")]
    [Area("Learning")]
    public class LearningController : Controller
    {
    }
}

namespace Workshop.Web.Areas.Resourses.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Workshop.Web.ViewModels.Resourses;

    [Authorize(Roles = "Learning")]
    [Area("Learning")]
    public class ResourseController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new List<ResourseViewModel>()
            {
                new ResourseViewModel()
                {
                    Name = "Basic Arrays",
                },

                new ResourseViewModel()
                {
                    Name = "Basic Arrays Exercice",
                },

                new ResourseViewModel()
                {
                    Name = "Test",
                },

                new ResourseViewModel()
                {
                    Name = "Test1",
                },
            };

            return this.View(viewModel);
        }
    }
}

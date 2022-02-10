namespace Workshop.Web.Areas.Resourses.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using Workshop.Web.ViewModels.Resourses;

    public class ResourseController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new List<ResourseModel>()
            {
                new ResourseModel()
                {
                    Name = "Basic Arrays",
                },

                new ResourseModel()
                {
                    Name = "Basic Arrays Exercice",
                },

                new ResourseModel()
                {
                    Name = "Test",
                },

                new ResourseModel()
                {
                    Name = "Test1",
                },
            };

            return this.View(viewModel);
        }
    }
}

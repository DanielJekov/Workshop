namespace Workshop.Web.Areas.Resourses.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Workshop.Web.ViewModels.Topics;

    [Authorize(Roles = "Learning")]
    [Area("Learning")]
    public class LearningController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new List<TopicViewModel>()
            {
                new TopicViewModel()
                {
                    Name = "Basic Arrays",
                },

                new TopicViewModel()
                {
                    Name = "Basic Arrays Exercice",
                },

                new TopicViewModel()
                {
                    Name = "Test",
                },

                new TopicViewModel()
                {
                    Name = "Test1",
                },
            };

            return this.View(viewModel);
        }
    }
}

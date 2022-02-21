namespace Workshop.Web.Areas.Learning.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Workshop.Services.Data.Topics;
    using Workshop.Services.InputModels.Topics;
    using Workshop.Web.Areas.Resourses.Controllers;
    using Workshop.Web.ViewModels.Topics;

    public class TopicsController : LearningController
    {
        private readonly ITopicsService topicsService;

        public TopicsController(ITopicsService topicsService)
        {
            this.topicsService = topicsService;
        }

        public IActionResult All(int courseId)
        {
            var viewModel = this.topicsService.GetAll<TopicViewModel>(courseId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> Create(TopicInputModel input)
        {
            await this.topicsService.CreateAsync(input);

            return this.Redirect("/");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.topicsService.DeleteAsync(id);

            return this.Redirect("/");
        }
    }
}

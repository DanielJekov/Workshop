namespace Workshop.Web.Areas.Learning.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Workshop.Services.Data.Topics;
    using Workshop.Services.InputDtos.Topics;
    using Workshop.Web.Areas.Resourses.Controllers;
    using Workshop.Web.ViewModels.Topics;

    public class TopicsController : LearningController
    {
        private readonly ITopicsService topicsService;

        public TopicsController(ITopicsService topicsService)
        {
            this.topicsService = topicsService;
        }

        [Route("Course/{courseId}/Topics")]
        public IActionResult All(int courseId)
        {
            var viewModel = this.topicsService.GetAll<TopicViewModel>(courseId);
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var inputDto = new TopicInputDto()
            {
                CourseId = input.CourseId,
                Name = input.Name,
                Note = input.Note,
                YoutubeLink = input.YoutubeLink,
            };

            await this.topicsService.CreateAsync(inputDto);

            return this.Redirect($"/Course/{input.CourseId}/Topics");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.topicsService.DeleteAsync(id);

            return this.Redirect("/");
        }
    }
}

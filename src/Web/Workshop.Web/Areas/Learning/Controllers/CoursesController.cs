namespace Workshop.Web.Areas.Learning.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Workshop.Services.Data.Courses;
    using Workshop.Web.Areas.Resourses.Controllers;
    using Workshop.Web.ViewModels.Courses;

    public class CoursesController : LearningController
    {
        private readonly ICoursesService coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        public IActionResult All()
        {
            var viewModel = this.coursesService.GetAll<CourseViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            await this.coursesService.CreateAsync(name);

            return this.RedirectToAction("All", "Courses", new { area = "Learning" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.coursesService.DeleteAsync(id);

            return this.Redirect("/");
        }
    }
}

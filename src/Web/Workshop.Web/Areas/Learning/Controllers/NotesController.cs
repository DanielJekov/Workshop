namespace Workshop.Web.Areas.Learning.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Workshop.Services.Data.Notes;
    using Workshop.Services.InputDtos.Notes;
    using Workshop.Web.Areas.Resourses.Controllers;
    using Workshop.Web.ViewModels.Notes;

    public class NotesController : LearningController
    {
        private readonly INotesService notesService;

        public NotesController(INotesService notesService)
        {
            this.notesService = notesService;
        }

        public IActionResult All()
        {
            var viewModel = this.notesService.GetAll<NoteViewModel>();

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var note = new NoteCreateInputDto()
            {
                Name = input.Name,
                Content = input.Content,
            };

            await this.notesService.CreateAsync(note);

            return this.Redirect("/");
        }
    }
}

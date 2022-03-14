namespace Workshop.Web.ViewModels.Notes
{
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class NoteViewModel : IMapFrom<Note>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

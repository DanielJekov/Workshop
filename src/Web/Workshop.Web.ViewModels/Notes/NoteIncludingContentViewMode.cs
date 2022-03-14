namespace Workshop.Web.ViewModels.Notes
{
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class NoteIncludingContentViewMode : IMapFrom<Note>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }
    }
}

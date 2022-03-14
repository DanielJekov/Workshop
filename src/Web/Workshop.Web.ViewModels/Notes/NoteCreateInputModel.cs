namespace Workshop.Web.ViewModels.Notes
{
    using System.ComponentModel.DataAnnotations;

    public class NoteCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }
    }
}

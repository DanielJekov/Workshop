namespace Workshop.Services.InputDtos.Notes
{
    using System.ComponentModel.DataAnnotations;

    public class NoteCreateInputDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }
    }
}

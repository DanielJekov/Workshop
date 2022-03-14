namespace Workshop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Workshop.Data.Common.Models;

    public class Note : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }
    }
}

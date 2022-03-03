namespace Workshop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Workshop.Data.Common.Models;

    public class Topic : BaseDeletableModel<int>
    {
        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public string Note { get; set; }

        [Required]
        [Url]
        public string YoutubeLink { get; set; }
    }
}

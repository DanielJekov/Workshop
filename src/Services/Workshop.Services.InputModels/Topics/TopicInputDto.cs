namespace Workshop.Services.InputDtos.Topics
{
    using System.ComponentModel.DataAnnotations;

    public class TopicInputDto
    {
        public int CourseId { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public string Note { get; set; }

        public string YoutubeLink { get; set; }
    }
}

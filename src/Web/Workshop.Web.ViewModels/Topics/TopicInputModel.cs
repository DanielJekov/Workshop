namespace Workshop.Web.ViewModels.Topics
{
    using System.ComponentModel.DataAnnotations;

    public class TopicInputModel
    {
        private string youtubeLink;

        public int CourseId { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public string Note { get; set; }

        public string YoutubeLink
        {
            get
            {
                return this.youtubeLink;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Replace("watch?v=", "embed/");
                    value = value.Replace("&list", "?list");
                    this.youtubeLink = value;
                }
            }
        }
    }
}

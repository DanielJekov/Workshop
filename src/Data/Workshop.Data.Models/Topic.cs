namespace Workshop.Data.Models
{
    using Workshop.Data.Common.Models;

    public class Topic : BaseDeletableModel<int>
    {
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public string YoutubeLink { get; set; }
    }
}

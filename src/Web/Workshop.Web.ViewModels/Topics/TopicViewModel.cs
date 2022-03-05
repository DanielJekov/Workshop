namespace Workshop.Web.ViewModels.Topics
{
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class TopicViewModel : IMapFrom<Topic>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Note { get; set; }

        public string YoutubeLink { get; set; }
    }
}

namespace Workshop.Web.ViewModels.Topics
{
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class TopicViewModel : IMapFrom<Topic>
    {
        public string Name { get; set; }

        public string Notes { get; set; }

        public string YoutubeLink { get; set; }
    }
}

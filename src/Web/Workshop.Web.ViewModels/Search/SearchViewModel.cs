namespace Workshop.Web.ViewModels.Search
{
    using System.Collections.Generic;

    using Workshop.Web.ViewModels.Courses;
    using Workshop.Web.ViewModels.Topics;

    public class SearchViewModel
    {
        public ICollection<Workshop.Web.ViewModels.Users.UserViewModel> Users { get; set; }

        public ICollection<CourseViewModel> Courses { get; set; }

        public ICollection<TopicViewModel> Topics { get; set; }
    }
}

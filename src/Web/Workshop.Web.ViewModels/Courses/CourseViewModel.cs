namespace Workshop.Web.ViewModels.Courses
{
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class CourseViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

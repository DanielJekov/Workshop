namespace Workshop.Web.ViewModels.Courses
{
    using System.ComponentModel.DataAnnotations;

    public class CourseInputModel
    {
        [Required]
        [MinLength(10)]
        public string Name { get; set; }
    }
}

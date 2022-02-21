namespace Workshop.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesService
    {
        IEnumerable<T> GetAll<T>();

        Task CreateAsync(string name);

        Task DeleteAsync(int id);
    }
}

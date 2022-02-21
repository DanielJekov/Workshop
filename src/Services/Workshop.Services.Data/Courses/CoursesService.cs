namespace Workshop.Services.Data.Courses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Workshop.Data.Common.Repositories;
    using Workshop.Data.Models;
    using Workshop.Services.Mapping;

    public class CoursesService : ICoursesService
    {
        private readonly IDeletableEntityRepository<Course> courseRepository;

        public CoursesService(IDeletableEntityRepository<Course> courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task CreateAsync(string name)
        {
            await this.courseRepository.AddAsync(new Course { Name = name });
            await this.courseRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = this.courseRepository.All().Where(x => x.Id == id).FirstOrDefault();
            this.courseRepository.Delete(course);
            await this.courseRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.courseRepository.AllAsNoTracking().To<T>().ToList();
        }
    }
}

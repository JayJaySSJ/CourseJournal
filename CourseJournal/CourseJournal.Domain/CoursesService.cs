using CourseJournal.Domain.Interfaces;
using CourseJournal.Domain.Models;
using System.Threading.Tasks;

namespace CourseJournal.Domain
{
    public interface ICoursesService
    {
        Task<bool> CreateNewAsync(Course newCourse);
    }

    public class CoursesService : ICoursesService
    {
        private readonly ICoursesRepository _coursesRepository;

        public CoursesService(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        public async Task<bool> CreateNewAsync(Course newCourse)
        {
            return await _coursesRepository.CreateNewAsync(newCourse);
        }
    }
}
using CourseJournal.Domain.Interfaces;
using CourseJournal.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.Domain
{
    public interface ICoursesService
    {
        Task<bool> CreateNewAsync(Course newCourse);
        Task<List<Course>> GetAllAsync();
        Task<bool> AddPresenceAsync(List<CoursePresence> coursePresence);
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

        public async Task<List<Course>> GetAllAsync() => await _coursesRepository.GetAllAsync();

        public async Task<bool> AddPresenceAsync(List<CoursePresence> coursePresence) => await _coursesRepository.AddPresenceAsync(coursePresence);
    }
}
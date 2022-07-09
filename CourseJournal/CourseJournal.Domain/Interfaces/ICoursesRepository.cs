using CourseJournal.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.Domain.Interfaces
{
    public interface ICoursesRepository
    {
        Task<bool> CreateNewAsync(Course newCourse);
        Task<List<Course>> GetAllAsync();
        Task<bool> AddTestResults(TestResults results);
    }
}
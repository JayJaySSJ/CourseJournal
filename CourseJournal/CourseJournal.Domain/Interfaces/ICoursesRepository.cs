using CourseJournal.Domain.Models;
using System.Threading.Tasks;

namespace CourseJournal.Domain.Interfaces
{
    public interface ICoursesRepository
    {
        Task<bool> CreateNewAsync(Course newCourse);
    }
}
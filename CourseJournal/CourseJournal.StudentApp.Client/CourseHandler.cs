using CourseJournal.StudentApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.StudentApp.Client
{
    public interface ICourseHandler
    {
        Task<List<Course>> ShowAllCourses();
        Task<bool> GiveEval();
    }

    public class CourseHandler : ICourseHandler
    {
        public Task<bool> GiveEval()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Course>> ShowAllCourses()
        {
            throw new System.NotImplementedException();
        }
    }
}

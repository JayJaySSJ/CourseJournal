using CourseJournal.StudentApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.StudentApp.Client.Clients
{
    public interface ICourseClient
    {
        Task<List<Course>> ShowAllCourses();
        Task<bool> GiveEval();
    }

    public class CourseClient : ICourseClient
    {
        public async Task<bool> GiveEval()
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Course>> ShowAllCourses()
        {
            throw new System.NotImplementedException();
        }
    }
}

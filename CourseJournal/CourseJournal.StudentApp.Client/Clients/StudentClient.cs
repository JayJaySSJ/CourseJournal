using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.StudentApp.Client.Clients
{
    public interface IStudentClient
    {
        Task<bool> Login(string username, string password);
        Task<bool> LogOut();
    }

    public class StudentClient : IStudentClient
    {
        public Task<bool> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogOut()
        {
            throw new NotImplementedException();
        }
    }
}

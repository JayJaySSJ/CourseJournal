using CourseJournal.AdminApp.Client.Clients;
using CourseJournal.AdminApp.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client
{
    internal interface IStudentHandler
    {
        Task AddStudent();
        Task<List<Student>> GetAllAsync();

        Task<Student> GetStudentById(int id);

    }

    internal class StudentHandler : IStudentHandler
    {
        private readonly IStudentsWebApiClient _studentsWebApiClient;
        private readonly ICliHelper _cliHelper;
        private readonly IConsoleManager _consoleManager;
        internal StudentHandler(ICliHelper cliHelper, IStudentsWebApiClient studentsWebApiClient, IConsoleManager consoleManager)
        {
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
            _studentsWebApiClient = studentsWebApiClient;
        }
        public async Task AddStudent()
        {
            Student student = new Student()
            {
                Name = _cliHelper.GetString("Get student name"),
                Surname = _cliHelper.GetString("Get student Surname"),
                Email = _cliHelper.GetString("Get student email"),
                Password = _cliHelper.GetPassword("Get Password"),
                BirthDate = _cliHelper.GetValidDateTime("Get date of Birth")

            };

            var existing = await _studentsWebApiClient.CheckIfExistEmail(student);
            if (existing)
            {
                Console.WriteLine("Student with this email exist adinng new student impossible, if you add new student choose 2 one more time ");
            }
            else
            {
                await _studentsWebApiClient.AddStudentAsync(student);
                Console.WriteLine("new student added successfully");
            }

        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentsWebApiClient.GetAllStudentsAsync();


        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _studentsWebApiClient.GetByIdAsync(id);
        }

    }


}

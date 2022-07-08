using CourseJournal.AdminApp.Client.Clients;
using CourseJournal.AdminApp.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client
{
    public interface ICoursesHandler
    {
        Task CreateNewAsync();
    }

    internal class CoursesHandler : ICoursesHandler
    {
        private readonly ICoursesClient _coursesClient;
        private readonly IConsoleManager _consoleManager;
        private readonly ICliHelper _cliHelper;
        private readonly IStudentHandler _studentHandler;
        private readonly ITrainersHandler _trainersHandler;
        public CoursesHandler(
            ICoursesClient coursesClient,
            IConsoleManager consoleManager,
            ICliHelper cliHelper, IStudentHandler studentHandler, ITrainersHandler trainersHandler)
        {
            _coursesClient = coursesClient;
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
            _studentHandler = studentHandler;
            _trainersHandler = trainersHandler;
        }

        public async Task CreateNewAsync()
        {
            _consoleManager.Clear();

            if (_trainersHandler.GetAllAsync() == null || _studentHandler.GetAllAsync()== null)
            {
                _consoleManager.WriteLine("(!) There are no students or trainers in the system. Come back again later...");

                return;
            }

            foreach (var trainer in await _trainersHandler.GetAllAsync())
            {
                _consoleManager.WriteLine($"Id: {trainer.Id}, name: {trainer.Name}, surname: {trainer.Surname}");               
            }
            var trainerId=  _cliHelper.GetInt("Get Id Choosen Trainer");

            foreach (var student in await _studentHandler.GetAllAsync())
            {
                _consoleManager.WriteLine($"Id: {student.Id}, name: {student.Name}, surname: {student.Surname}");
            }


            List<Student> studentsList = new List<Student>();
            bool NotaddNew = false;
            while (NotaddNew==false)
            {
                var id= _cliHelper.GetInt("Choose student to Course, get id choosen student");

                var student= _studentHandler.GetStudentById(id).Result;
                if (!studentsList.Contains(student))
                {
                    studentsList.Add(student);
                }
                
                NotaddNew = _cliHelper.GetBool("If you want finish adding students write true, if you want continue write false");
            };

            var newCourse = new Course
            {
                Name = _cliHelper.GetString("name of course"),
                StartDate = _cliHelper.GetValidDateTime("start date"),
                TrainerId = trainerId,
                Students = studentsList,
                PresenceThreshold = GetThreshold("presence"),
                HwResultsThreshold = GetThreshold("homeworks"),
                WtResultsThreshold = GetThreshold("tests")
            };

            var result = await _coursesClient.CreateNewAsync(newCourse);

            var defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = _cliHelper.GetConsoleColor(result, defaultColor);

            var message = result
                ? "New Course created successfully"
                : "(!) Error while creating new course";
            _consoleManager.WriteLine(message);

            Console.ForegroundColor = defaultColor;
        }

        internal int GetThreshold(string message)
        {
            var defaultThreshold = 70;

            if (_cliHelper.GetBool($"Do you want to create a custom threshold for {message}? [Default is: {defaultThreshold}]"))
            {
                return _cliHelper.GetIntInRange($"Provide {message} threshold", 0, 100);
            }

            return defaultThreshold;
        }
    }
}
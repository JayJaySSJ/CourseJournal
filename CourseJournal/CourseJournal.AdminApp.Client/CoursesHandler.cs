using CourseJournal.AdminApp.Client.Clients;
using CourseJournal.AdminApp.Client.Models;
using System;
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

        public CoursesHandler(
            ICoursesClient coursesClient,
            IConsoleManager consoleManager,
            ICliHelper cliHelper)
        {
            _coursesClient = coursesClient;
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
        }

        public async Task CreateNewAsync()
        {
            _consoleManager.Clear();

            //if (_trainersHandler.GetAllAsync().Length == 0 || _studentsHandler.GetAllAsync().Length == 0)
            //{
            //    _consoleManager.WriteLine("(!) There are no students or trainers in the system. Come back again later...");

            //    return;
            //}

            var newCourse = new Course
            {
                Name = _cliHelper.GetString("name"),
                StartDate = _cliHelper.GetValidDateTime("start date"),
                //TrainerId = _trainersHandler.GetTrainerIdAsync(),
                //Students = _studentsHandler.GetStudentsAsync(),
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
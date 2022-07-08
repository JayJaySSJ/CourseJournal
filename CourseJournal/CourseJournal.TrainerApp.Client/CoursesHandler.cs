using CourseJournal.TrainerApp.Client.Clients;
using CourseJournal.TrainerApp.Client.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseJournal.TrainerApp.Client
{
    public interface ICoursesHandler
    {
        Task<Course> GetTrainersCourse(Trainer trainer);
    }

    public class CoursesHandler : ICoursesHandler
    {
        private readonly ICoursesClient _coursesClient;
        private readonly IConsoleManager _consoleManager;
        private readonly ICliHelper _cliHelper;

        public CoursesHandler(
            IConsoleManager consoleManager,
            ICoursesClient coursesClient,
            ICliHelper cliHelper)
        {
            _consoleManager = consoleManager;
            _coursesClient = coursesClient;
            _cliHelper = cliHelper;
        }

        public async Task<Course> GetTrainersCourse(Trainer trainer)
        {
            var allCourses = await _coursesClient.GetAllAsync();
            var trainersCourses = allCourses
                .Where(x => x.TrainerId == trainer.Id)
                .ToList();

            if (trainersCourses.Count == 0)
            {
                _consoleManager.WriteLine($"(!) There are no courses of trainer: {trainer.Id}. {trainer.Name}");
                return null;
            }

            _consoleManager.WriteLine("Pick course's Id from listed below:\n");

            var ids = new List<int>();

            foreach(var course in trainersCourses)
            {
                ids.Add(course.Id);

                _consoleManager.WriteLine($" [ID: {course.Id}] {course.Name}");
            }

            var id = _cliHelper.GetInt("\nPick");

            if (ids.Contains(id))
            {
                return trainersCourses[id];
            }

            _consoleManager.WriteLine($"(!) There are no courses under given Id [{id}]");
            return null;
        }
    }
}
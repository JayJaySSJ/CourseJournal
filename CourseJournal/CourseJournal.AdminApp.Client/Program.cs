using CourseJournal.AdminApp.Client.Clients;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client
{
    internal class Program
    {
        static async Task Main()
        {
            ConsoleManager consoleManager = new ConsoleManager();
            CliHelper cliHelper = new CliHelper(consoleManager);
            CoursesClient coursesClient = new CoursesClient();
            TrainerClient trainerClient = new TrainerClient();
            StudentsWebApiClient studentsWebApiClient = new StudentsWebApiClient();
            StudentHandler studentHandler = new StudentHandler(cliHelper, studentsWebApiClient, consoleManager);
            TrainersHandler trainersHandler = new TrainersHandler(consoleManager, cliHelper, trainerClient);
            CoursesHandler coursesHandler = new CoursesHandler(coursesClient, consoleManager, cliHelper, studentHandler, trainersHandler);
            ActionsHandler actionsHandler = new ActionsHandler(consoleManager, trainersHandler, cliHelper, coursesHandler, studentHandler);
            await actionsHandler.ProgramLoop();
        } 
    }
}
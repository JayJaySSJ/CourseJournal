using CourseJournal.TrainerApp.Client.Clients;
using System.Threading.Tasks;

namespace CourseJournal.TrainerApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var trainersClient = new TrainerSClient();
            var consoleManager = new ConsoleManager();
            var cliHelper = new CliHelper(consoleManager);
            var coursesHandler = new CoursesHandler(consoleManager, new CoursesClient(), cliHelper);
            var actionsHandler = new ActionsHandler(consoleManager, cliHelper, trainersClient, coursesHandler);
            await actionsHandler.ProgramLoop();
        }
    }
}
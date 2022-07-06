using CourseJournal.AdminApp.Client.Clients;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client
{
    internal class Program
    {
        private static readonly ActionsHandler _actionsHandler = new ActionsHandler(
            new ConsoleManager(), 
            new CliHelper(
                new ConsoleManager()), 
            new CoursesHandler(
                new CoursesClient(), new ConsoleManager(), new CliHelper(new ConsoleManager())));

        static async Task Main(string[] args)
        {
            await _actionsHandler.ProgramLoop();
        }
    }
}
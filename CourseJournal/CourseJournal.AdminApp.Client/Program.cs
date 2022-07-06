using CourseJournal.AdminApp.Client.Clients;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client
{
    internal class Program
    {
        private static readonly ActionsHandler _actionsHandler = new ActionsHandler(new ConsoleManager(), new CliHelper(new ConsoleManager()), new StudentHandler(new CliHelper(new ConsoleManager()), new StudentsWebApiClient(), new ConsoleManager()));

        static async Task Main(string[] args)
        {
           await _actionsHandler.ProgramLoop();
        }
    }
}
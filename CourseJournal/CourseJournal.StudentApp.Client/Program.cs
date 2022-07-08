using CourseJournal.StudentApp.Client.Clients;
using System.Threading.Tasks;

namespace CourseJournal.StudentApp.Client
{
    internal class Program
    {
        private static readonly LoginHandler _loginHandler = new LoginHandler(new CliHelper(new ConsoleManager()), new ConsoleManager(), new StudentClient());
        private static readonly ActionsHandler _actionsHandler = new ActionsHandler(new CliHelper(new ConsoleManager()), new ConsoleManager(), new LoginHandler(new CliHelper(new ConsoleManager()), new ConsoleManager(), new StudentClient()), new CourseHandler());

        static async Task Main()
        {
            string loggedUser = await _loginHandler.Login();

            if (!string.IsNullOrEmpty(loggedUser))
            {
                await _actionsHandler.ProgramLoop(loggedUser);
            }
        }
    }
}

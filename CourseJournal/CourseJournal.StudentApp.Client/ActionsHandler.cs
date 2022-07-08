using CourseJournal.StudentApp.Client.Clients;
using System;
using System.Threading.Tasks;

namespace CourseJournal.StudentApp.Client
{
    public interface IActionsHandler
    {
        Task ProgramLoop(string loggedUser);
    }

    internal class ActionsHandler : IActionsHandler
    {
        private readonly ICliHelper _cliHelper;
        private readonly IConsoleManager _consoleManager;
        private readonly ILoginHandler _loginHandler;
        private readonly ICourseHandler _courseHandler;

        public ActionsHandler(
            ICliHelper cliHelper, 
            IConsoleManager consoleManager, 
            ILoginHandler loginhandler, 
            ICourseHandler courseHandler)
        {
            _cliHelper = cliHelper;
            _consoleManager = consoleManager;
            _loginHandler = loginhandler;
            _courseHandler = courseHandler;
        }

        public async Task ProgramLoop(string loggedUser)
        {
            try
            {
                var exit = false;
                while (!exit)
                {
                    _consoleManager.WriteLine("\nPick number to choose action:");
                    _consoleManager.WriteLine("" +
                        " 0 - show all courses\n" +
                        " 1 - give course evaluation\n" +
                        " 2 - log out");

                    var switcher = _cliHelper.GetInt("Your pick");
                    switch (switcher)
                    {
                        case 0:
                            await _courseHandler.ShowAllCourses();
                            break;
                        case 1:
                            await _courseHandler.GiveEval();
                            break;
                        case 2:
                            await _loginHandler.LogOut();
                            _consoleManager.WriteLine("Adios");
                            exit = true;
                            break;
                        default:
                            _consoleManager.Clear();
                            _consoleManager.WriteLine("(!) Please write crorrect number from menu");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _consoleManager.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}

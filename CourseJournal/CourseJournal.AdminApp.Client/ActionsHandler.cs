using System;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client
{
    internal interface IActionsHandler
    {
        Task ProgramLoop();
    }

    internal class ActionsHandler : IActionsHandler
    {
        private readonly IConsoleManager _consoleManager;
        private readonly ICliHelper _cliHelper;
        private readonly IStudentHandler _studentHandler;
        public ActionsHandler(
            IConsoleManager consoleManager,
            ICliHelper cliHelper,
            IStudentHandler studentHandler)
        {
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
            _studentHandler = studentHandler;
        }

        public async Task ProgramLoop()
        {
            try
            {
                var exit = false;
                while (!exit)
                {
                    _consoleManager.WriteLine("\nPick number to choose action:");
                    _consoleManager.WriteLine("" +
                        " 0 - exit\n" +
                        " 2 - Get Student data");

                    var switcher = _cliHelper.GetInt("Your pick");
                    switch (switcher)
                    {
                        case 0:
                            _consoleManager.WriteLine("Adios");
                            exit = true;
                            break;
                        case 2:
                            _consoleManager.WriteLine("Get student data, Name, Surname, Email, Password, itd");
                            await _studentHandler.AddStudent();
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
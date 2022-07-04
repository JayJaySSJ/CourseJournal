using System;

namespace CourseJournal.AdminApp.Client
{
    internal class ActionsHandler
    {
        private readonly IConsoleManager _consoleManager;
        private readonly ICliHelper _cliHelper;

        public ActionsHandler(
            IConsoleManager consoleManager,
            ICliHelper cliHelper)
        {
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
        }

        public void ProgramLoop()
        {
            try
            {
                var exit = false;
                while (!exit)
                {
                    _consoleManager.WriteLine("\nPick number to choose action:");
                    _consoleManager.WriteLine("" +
                        " 0 - exit\n" +
                        " 1 - print readings\n" +
                        " 2 - print power readings\n");

                    var switcher = _cliHelper.GetInt("Your pick");
                    switch (switcher)
                    {
                        case 0:
                            _consoleManager.WriteLine("Adios");
                            exit = true;
                            break;
                        case 1:
                            
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


using CourseJournal.TrainerApp.Client;
using System;
using System.Threading.Tasks;

namespace CourseJournal.Trainer.Client
{
    internal interface IActionsHandler
    {
        Task ProgramLoop();
    }

    internal class ActionsHandler : IActionsHandler
    {
        private readonly IConsoleManager _consoleManager;
        private readonly ICliHelper _cliHelper;
        
        private readonly ITrainersHandler _trainersHandler;
        


        public ActionsHandler(
            IConsoleManager consoleManager,
            ITrainersHandler trainersHandler,
            ICliHelper cliHelper
           
            )
        {
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
            _trainersHandler = trainersHandler;
           
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
                        " 1 - Login trainer\n");
                        
                     

                    var switcher = _cliHelper.GetInt("Your pick");
                    switch (switcher)
                    {
                        case 0:
                            _consoleManager.WriteLine("Adios");
                            exit = true;
                            break;
                        case 1:
                            await _trainersHandler.LoginTrainer();
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

using CourseJournal.Trainer.Client;
using CourseJournal.TrainerApp.Client.Clients;
using CourseJournal.TrainerApp.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.TrainerApp.Client
{

    internal interface ITrainersHandler
    {      
        Task<bool> LoginTrainer();
    }

    internal class TrainersHandler : ITrainersHandler
    {
        private readonly IConsoleManager _consoleManager;
        private readonly ICliHelper _cliHelper;
        private readonly ITrainerClient _trainerClient;

        public TrainersHandler(IConsoleManager consoleManager, ICliHelper cliHelper, ITrainerClient trainerClient)
        {
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
            _trainerClient = trainerClient;
        }
          
        public async Task<bool> LoginTrainer()
        {                     
            TrainerModel loginTrainer = new TrainerModel()
            {
                Name = _cliHelper.GetString("Name"),
                Password = _cliHelper.GetPassword("Password"),
            };
            var loginSuccess = await _trainerClient.LoginTrainer(loginTrainer);
            if (loginSuccess)
            {
                _consoleManager.WriteLine($"Trainer {loginTrainer.Name}  loged successfully.");
            }
            return  loginSuccess;
        }        
    }
}

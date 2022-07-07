using CourseJournal.AdminApp.Client.Clients;
using CourseJournal.AdminApp.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client
{
    internal interface ITrainersHandler
    {
        Task<bool> CreateTrainer();
        Task<List<Trainer>> GetAllAsync();

        Task<Trainer> GetTrainerById(int id);
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

        public async Task<bool> CreateTrainer()
        {
            var email = _cliHelper.GetString("Enter email");
            bool emailExists = await _trainerClient.CheckIfExists(email);

            if (emailExists)
            {
                _consoleManager.WriteLine($"Address {email} already exists. Choose different email address that is unique.");
                return false;
            }
            else
            {
                Trainer newTrainer = new Trainer()
                {
                    Name = _cliHelper.GetString("Name"),
                    Surname = _cliHelper.GetString("Surname"),
                    Email = email,
                    Password = _cliHelper.GetPassword("Password"),
                    BirthDate = _cliHelper.GetValidDateTime("BirthDate")
                };

                _consoleManager.WriteLine($"Trainer {newTrainer.Name} {newTrainer.Surname} created successfully.");

                return await _trainerClient.CreateTrainer(newTrainer);
            }
        }

        public async Task<List<Trainer>> GetAllAsync()
        {
            return await _trainerClient.GetAllAsync();
        }

        public async Task<Trainer> GetTrainerById(int id)
        {
            return await _trainerClient.GetTrainer(id);
        }

      
    }
}

using CourseJournal.AdminApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.Domain
{
    public interface ITrainersService
    {
        Task<bool> CreateTrainer(Trainer trainer);
        Task<bool> CheckIfExists(string email);
        Task<List<Trainer>> GetAllAsync();
        Task<Trainer> GetTrainerByIdAsync(int id);

        Task<bool> LoginTrainer(Trainer trainer);
    }

    public class TrainersService : ITrainersService
    {
        private readonly ITrainersRepository _trainersRepository;

        private static Trainer _loggedTrainer = null;

        public TrainersService(ITrainersRepository trainersRepository)
        {
            _trainersRepository = trainersRepository;
        }

        public async Task<bool> CreateTrainer(Trainer trainer)
        {
            return await _trainersRepository.CreateTrainer(trainer);
        }

        public async Task<bool> CheckIfExists(string email)
        {
            var trainer = await _trainersRepository.GetTrainer(email);

            return trainer != null;
        }

        public async Task<List<Trainer>> GetAllAsync()
        {
            return await _trainersRepository.GetAllAsync();
        }

        public async Task<Trainer> GetTrainerByIdAsync(int id)
        {
            return await _trainersRepository.GetTrainerById(id);
        }

      
        public async Task<bool> LoginTrainer(Trainer trainer)
        {
            var trainerToLogin = await _trainersRepository.GetTrainerByName(trainer);

            if (trainer.Password == trainerToLogin.Password)
            {
                _loggedTrainer = await GetTrainerByIdAsync(trainer.Id);

                return true;
            }

            return false;
        }
    }
}
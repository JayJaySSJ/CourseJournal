using CourseJournal.AdminApp.Domain.Models;
using System.Threading.Tasks;

namespace CourseJournal.Domain
{
    public interface ITrainersService
    {
        Task<bool> CreateTrainer(Trainer trainer);
        Task<bool> CheckIfExists(string email);
    }

    public class TrainersService : ITrainersService
    {
        private readonly ITrainersRepository _trainersRepository;

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
    }
}

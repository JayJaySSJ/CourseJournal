using CourseJournal.AdminApp.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseJournal.Domain
{
    public interface ITrainersRepository
    {
        Task<bool> CreateTrainer(Trainer trainer);
        Task<Trainer> GetTrainer(string email);
        Task<List<Trainer>> GetAllAsync();
        Task<Trainer> GetTrainerById(int id);

        Task<Trainer> GetTrainerByName(Trainer trainer);

    }
}
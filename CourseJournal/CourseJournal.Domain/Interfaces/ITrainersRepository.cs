using CourseJournal.AdminApp.Domain.Models;
using System.Threading.Tasks;

namespace CourseJournal.Domain
{
    public interface ITrainersRepository
    {
        Task<bool> CreateTrainer(Trainer trainer);
        Task<Trainer> GetTrainer(string email);
    }
}
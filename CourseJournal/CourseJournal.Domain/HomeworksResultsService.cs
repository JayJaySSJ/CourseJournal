using CourseJournal.Domain.Interfaces;
using CourseJournal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.Domain
{
    public interface IHomeworksResultsService
    {
        Task<bool> AddHomeworkResult(HomeworkResult homeworkResult);
    }

    public class HomeworksResultsService : IHomeworksResultsService
    {
        IHomeworksResultsRpository _homeworksResultsRepository;
        public HomeworksResultsService(IHomeworksResultsRpository homeworksResultsRpository)
        {
            _homeworksResultsRepository = homeworksResultsRpository;
        }
        public async Task<bool> AddHomeworkResult(HomeworkResult homeworkResult)
        {
            return await _homeworksResultsRepository.CreateHomeworkResult(homeworkResult);
        }
    }
}

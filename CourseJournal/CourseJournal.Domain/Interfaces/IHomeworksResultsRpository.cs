using CourseJournal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.Domain.Interfaces
{
    public interface IHomeworksResultsRpository
    {
        Task<bool> CreateHomeworkResult(HomeworkResult homeworkResult);
    }
}

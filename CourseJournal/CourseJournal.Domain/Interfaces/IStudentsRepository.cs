using CourseJournal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.Domain.Interfaces
{
    public interface IStudentsRepository
    {
        Task<bool> AddAsync(Student student);
        Task<List<Student>> GetAllAsync();

        Task<Student> GetByIdAsync(int id);
    }
}

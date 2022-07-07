using CourseJournal.Domain.Interfaces;
using CourseJournal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.Domain
{
    public interface IStudentService
    {
        Task<bool> AddAsync(Student student);

        Task<bool> CheckExistedEmail(Student student);
        Task<List<Student>> GetAllAsync();

        Task<Student> GetByIdAsync(int id);
    }
    public class StudentService:IStudentService
    {
        IStudentsRepository _studentsRepository;
        public StudentService(IStudentsRepository studentsRepository)
        {
            _studentsRepository=studentsRepository;
        }

        public async Task<List<Student>> GetAllAsync()
        {
           return await _studentsRepository.GetAllAsync();
        }

        public async Task<bool>  CheckExistedEmail(Student student)
        {
            List<Student> students = await _studentsRepository.GetAllAsync();
            bool existedEmail = false;
            foreach (var studentFromBase in students)
            {
                
                if (student.Email == studentFromBase.Email)
                {
                    return existedEmail = true;
                   // break;
                }
                else
                {
                    existedEmail = false;
                    
                }
            }
            return existedEmail;
        }


        public async Task<bool> AddAsync(Student student)
        {
           var ifExistedEmail=CheckExistedEmail(student).Result;
          
            if (student.Password.Length<6 || ifExistedEmail==true)
            {
                return false;
            }
            return await _studentsRepository.AddAsync(student);
        }

        public async Task<Student> GetByIdAsync(int id) => await _studentsRepository.GetByIdAsync(id);

    }
  
}

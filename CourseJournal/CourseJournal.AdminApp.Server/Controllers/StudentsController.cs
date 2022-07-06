using CourseJournal.Domain;
using CourseJournal.Domain.Models;
using CourseJournal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseJournal.AdminApp.Server.Controllers
{
    [RoutePrefix("api/v1/students")]
    public class StudentsController : ApiController
    {

        private readonly IStudentService _studentService;
        public StudentsController()
        {
            _studentService = new StudentService(new StudentsRepository());
        }
        [HttpPost]
        [Route("create")]
        public async Task<bool> Create([FromBody] Student student)
        {
            return await _studentService.AddAsync(student);
        }

        [HttpPost]
        [Route("check")]
        public async Task<bool> CheckIfExist([FromBody] Student student)
        {
            return await _studentService.CheckExistedEmail(student);  
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<Student> GetByIdAsync(int id) => await _studentService.GetByIdAsync(id);

    }
}

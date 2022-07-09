using CourseJournal.Domain;
using CourseJournal.Domain.Models;
using CourseJournal.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseJournal.AdminApp.Server.Controllers
{
    [RoutePrefix("api/v1/courses")]
    public class CoursesController : ApiController
    {
        private readonly ICoursesService _coursesService;
        private readonly IHomeworksResultsService _homeworksResultsService;

        public CoursesController()
        {
            _coursesService = new CoursesService(new CoursesRepository());
            _homeworksResultsService = new HomeworksResultsService(new HomeworksResultsRepository());
        }

        [HttpPost]
        [Route("")]
        public async Task<bool> CreateAsync([FromBody] Course newCourse) => await _coursesService.CreateNewAsync(newCourse);

        [HttpGet]
        [Route("")]
        public async Task<List<Course>> GetAllAsync() => await _coursesService.GetAllAsync();


        [HttpPost]
        [Route("addresulthomework")]
        public async Task<bool> AddResultHomeworkAsync([FromBody] HomeworkResult homeworkResult) => await _homeworksResultsService.AddHomeworkResult(homeworkResult);
    }

}

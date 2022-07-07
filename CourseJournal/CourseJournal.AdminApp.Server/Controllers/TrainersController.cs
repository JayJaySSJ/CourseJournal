using CourseJournal.AdminApp.Domain.Models;
using CourseJournal.Domain;
using CourseJournal.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseJournal.AdminApp.Server.Controllers
{
    [RoutePrefix("api/v1/trainers")]
    public class TrainersController : ApiController
    {
        private readonly ITrainersService _trainersService;

        public TrainersController()
        {
            _trainersService = new TrainersService(new TrainersRepository());
        }

        [HttpPost()]
        [Route("")]
        public async Task<bool> Create([FromBody] Trainer trainer)
        {
            return await _trainersService.CreateTrainer(trainer);
        }

        [HttpPost()]
        [Route("checkIfExists")]
        public async Task<bool> CheckIfExists([FromBody] string email)
        {
            return await _trainersService.CheckIfExists(email);
        }

        [HttpGet]
        [Route("")]
        public async Task<List<Trainer>> GetAllAsync() => await _trainersService.GetAllAsync();

        [HttpGet]
        [Route("{id}")]
        public async Task<Trainer> GetTrainer(int id) => await _trainersService.GetTrainer(id);
    }
}

using CourseJournal.AdminApp.Domain.Models;
using CourseJournal.Domain;
using CourseJournal.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;

namespace CourseJournal.TrainerApp.Server.Controllers
{
    [RoutePrefix("api/v1/trainer")]
    public class TrainersController
    {
        private readonly ITrainersService _trainersService;

        public TrainersController()
        {
            _trainersService = new TrainersService(new TrainersRepository());
        }
        [HttpPost("login")]
        //[Route()]
        public async Task<bool> CheckIfExists([FromBody] Trainer trainer)
        {
            return await _trainersService.LoginTrainer(trainer);
        }
    }
}
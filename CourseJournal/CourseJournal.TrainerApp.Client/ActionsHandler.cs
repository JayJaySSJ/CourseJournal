using CourseJournal.TrainerApp.Client.Clients;
using CourseJournal.TrainerApp.Client.Models;
using System;
using System.Threading.Tasks;

namespace CourseJournal.TrainerApp.Client
{
    internal interface IActionsHandler
    {
        Task ProgramLoop();
    }

    internal class ActionsHandler : IActionsHandler
    {
        private readonly IConsoleManager _consoleManager;
        private readonly ICliHelper _cliHelper;
        private readonly ITrainersClient _trainersClient;
        private readonly ICoursesHandler _coursesHandler;

        private static Course _activeCourse = null;

        public ActionsHandler(
            IConsoleManager consoleManager,
            ICliHelper cliHelper,
            ITrainersClient trainersClient,
            ICoursesHandler coursesHandler
            )
        {
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
           _trainersClient = trainersClient;
            _coursesHandler = coursesHandler;
        }

        public async Task ProgramLoop()
        {
            try
            {
                var exit = false;
                while (!exit)
                {
                    _consoleManager.Clear();
                    _consoleManager.WriteLine("\nPick number to choose action:");
                    _consoleManager.WriteLine("" +
                        " 0 - Exit\n" +
                        " 1 - Login\n");

                    var switcher = _cliHelper.GetInt("Your pick");
                    switch (switcher)
                    {
                        case 0:
                            _consoleManager.WriteLine("Adios");
                            exit = true;
                            break;
                        case 1:
                            await Login();
                            break;
                       
                        default:
                            _consoleManager.Clear();
                            _consoleManager.WriteLine("(!) Please write crorrect number from menu");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _consoleManager.WriteLine($"ERROR: {ex.Message}");
            }
        }

        public async Task Login()
        {
            var inputTrainer = new Trainer();
            var flag = false;

            while (!flag)
            {
                _consoleManager.Clear();
                _consoleManager.WriteLine("Provide credentials to LogIn:\n");

                inputTrainer = new Trainer()
                {
                    Name = _cliHelper.GetString("Name"),
                    Password = _cliHelper.GetPassword("Password"),
                };

                var loginResult = await _trainersClient.LoginTrainer(inputTrainer);

                if (loginResult)
                {
                    _consoleManager.WriteLine($"Trainer {inputTrainer.Name} logged successfully.");

                    flag = true;
                }
            }

            inputTrainer = await _trainersClient.GetByNameAsync(inputTrainer.Name);

            await TrainersConsole(inputTrainer);
        }

        public async Task TrainersConsole(Trainer inputTrainer)
        {
            try
            {
                var exit = false;
                while (!exit)
                {
                    _consoleManager.Clear();
                    if(_activeCourse != null)
                    {
                        var trainer = await _trainersClient.GetTrainerByIdAsync(_activeCourse.TrainerId);

                        _consoleManager.WriteLine($"[Active Course: {_activeCourse.Name}; Trainer: {trainer.Name} {trainer.Surname}; Start Date: {_activeCourse.StartDate}]");
                    }

                    _consoleManager.WriteLine("\nPick number to choose action:");
                    _consoleManager.WriteLine("" +
                        " 0 - Log Out\n" +
                        " 1 - Pick Course\n");

                    var switcher = _cliHelper.GetInt("Your pick");
                    switch (switcher)
                    {
                        case 0:
                            await ProgramLoop();
                            exit = true;
                            break;
                        case 1:
                            _activeCourse = await _coursesHandler.GetTrainersCourse(inputTrainer);
                            break;
                        case 2:
                            await _coursesHandler.AddTestResults(_activeCourse);
                            break;
                        default:
                            _consoleManager.Clear();
                            _consoleManager.WriteLine("(!) Please write crorrect number from menu");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                _consoleManager.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
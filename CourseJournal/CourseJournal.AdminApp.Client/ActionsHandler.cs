using CourseJournal.AdminApp.Client.Clients;
using CourseJournal.AdminApp.Client.Models;
using System;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client
{
    internal interface IActionsHandler
    {
        Task ProgramLoop();
    }

    internal class ActionsHandler : IActionsHandler
    {
        private readonly IConsoleManager _consoleManager;
        private readonly ICliHelper _cliHelper;
        private readonly IStudentHandler _studentHandler;
        private readonly ITrainersHandler _trainersHandler;
        private readonly ICoursesHandler _coursesHandler;


        public ActionsHandler(
            IConsoleManager consoleManager,
            ITrainersHandler trainersHandler,
            ICliHelper cliHelper,
            ICoursesHandler coursesHandler,
            IStudentHandler studentHandler)
        {
            _consoleManager = consoleManager;
            _cliHelper = cliHelper;
            _trainersHandler = trainersHandler;
            _coursesHandler = coursesHandler;
            _studentHandler = studentHandler;
        }

        public async Task ProgramLoop()
        {
            try
            {
                var exit = false;
                while (!exit)
                {
                    _consoleManager.WriteLine("\nPick number to choose action:");
                    _consoleManager.WriteLine("" +
                        " 0 - exit\n" +
                        " 1 - Create a new trainer\n" +
                        " 2 - Get Student data\n" +
                        " 3 - Create a new Course");

                    var switcher = _cliHelper.GetInt("Your pick");
                    switch (switcher)
                    {
                        case 0:
                            _consoleManager.WriteLine("Adios");
                            exit = true;
                            break;
                        case 1:
                            await _trainersHandler.CreateTrainer();
                            break;
                        case 2:
                            _consoleManager.WriteLine("Get student data, Name, Surname, Email, Password, itd");
                            await _studentHandler.AddStudent();
                            break;
                        case 3:
                            await _coursesHandler.CreateNewAsync();
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
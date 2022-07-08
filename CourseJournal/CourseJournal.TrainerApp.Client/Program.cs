

using CourseJournal.Trainer.Client;
using CourseJournal.TrainerApp.Client.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.TrainerApp.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TrainerClient trainerClient = new TrainerClient();
            ConsoleManager consoleManager = new ConsoleManager();
            CliHelper cliHelper = new CliHelper(consoleManager);
            TrainersHandler trainersHandler = new TrainersHandler(consoleManager, cliHelper, trainerClient);
            ActionsHandler actionsHandler = new ActionsHandler(consoleManager, trainersHandler, cliHelper);
            await actionsHandler.ProgramLoop();
        }
    }
}

namespace CourseJournal.AdminApp.Client
{
    internal class Program
    {
        private static readonly ActionsHandler _actionsHandler = new ActionsHandler(new ConsoleManager(), new CliHelper(new ConsoleManager()));

        static void Main(string[] args)
        {
            _actionsHandler.ProgramLoop();
        }
    }
}
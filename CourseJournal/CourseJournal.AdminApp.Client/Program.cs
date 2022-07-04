namespace CourseJournal.AdminApp.Client
{
    internal class Program
    {
        private static ActionsHandler _actionsHandler;

        static void Main(string[] args)
        {
            _actionsHandler.ProgramLoop();
        }
    }
}
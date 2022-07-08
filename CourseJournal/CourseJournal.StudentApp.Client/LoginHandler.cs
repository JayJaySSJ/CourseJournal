using CourseJournal.StudentApp.Client.Clients;
using System.Threading.Tasks;

namespace CourseJournal.StudentApp.Client
{
    public interface ILoginHandler
    {
        Task<string> Login();
        Task<bool> LogOut();
    }
    internal class LoginHandler : ILoginHandler
    {
        private readonly ICliHelper _cliHelper;
        private readonly IConsoleManager _consoleManager;
        private readonly IStudentClient _studentClient;

        public LoginHandler(ICliHelper cliHelper, IConsoleManager consoleManager, IStudentClient studentClient)
        {
            _cliHelper = cliHelper;
            _consoleManager = consoleManager;
            _studentClient = studentClient;
        }

        public async Task<string> Login()
        {
            string username = _cliHelper.GetString("Add username");
            string password = _cliHelper.GetString("Add pasword");

            bool correctCredentials = await _studentClient.Login(username, password);

            if (correctCredentials)
            {
                _consoleManager.WriteLine($"Login successful. Hello {username}");
            }
            else
            {
                _consoleManager.WriteLine($"Login unsuccesful. Try again...");
                return null;
            }

            return username;
        }

        public async Task<bool> LogOut()
        {
            throw new System.NotImplementedException();
        }
    }
}

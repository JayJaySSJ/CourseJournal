using System;
using System.Globalization;

namespace CourseJournal.AdminApp.Client
{
    internal interface ICliHelper
    {
        bool GetBool(string message);
        ConsoleColor GetConsoleColor(bool switcher, ConsoleColor defaultColor);
        int GetInt(string message);
        string GetString(string message);
        DateTime GetValidDateTime(string rangePoint);
        string GetPassword(string message);
    }

    internal class CliHelper : ICliHelper
    {
        private readonly IConsoleManager _consoleManager;

        public CliHelper(IConsoleManager consoleManager)
        {
            _consoleManager = consoleManager;
        }

        public string GetString(string message)
        {
            _consoleManager.Write($"{message}: ");
            var stringOut = _consoleManager.ReadLine();
            while (string.IsNullOrEmpty(stringOut))
            {
                _consoleManager.Clear();
                _consoleManager.WriteLine($"(!) Invalid string, please try again..\n");
                _consoleManager.Write($"{message}: ");
                stringOut = _consoleManager.ReadLine();
            }
            return stringOut;
        }

        public int GetInt(string message)
        {
            int intOut;

            while (!int.TryParse(GetString(message), out intOut))
            {
                _consoleManager.Clear();
                _consoleManager.WriteLine("(!) Write number\n");
            }

            return intOut;
        }

        public DateTime GetValidDateTime(string rangePoint)
        {
            DateTime output;

            while (!DateTime.TryParseExact(
                GetString($"Provide valid {rangePoint} [MM/dd/yyyy]"),
                "MM/dd/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out output))
            {
                Console.Clear();
                Console.WriteLine("(!) Invalid date provided, try again...\n");
            }

            return output;
        }

        public ConsoleColor GetConsoleColor(bool switcher, ConsoleColor defaultColor)
        {
            switch (switcher)
            {
                case true:
                    return ConsoleColor.Green;
                case false:
                    return ConsoleColor.Red;
                default:
                    return defaultColor;
            }
        }

        public bool GetBool(string message)
        {
            bool boolOut;

            while (!bool.TryParse(GetString(message), out boolOut))
            {
                _consoleManager.Clear();
                _consoleManager.WriteLine("(!) Write true/false\n");
            }

            return boolOut;
        }

        public string GetPassword(string message)
        {
            _consoleManager.Write($"{message}: ");
            var stringOut = _consoleManager.ReadLine();

            while (string.IsNullOrEmpty(stringOut) || stringOut.Length < 6)
            {
                _consoleManager.Clear();
                _consoleManager.WriteLine($"(!) Invalid string, please try again..\n");
                _consoleManager.Write($"{message}: ");
                stringOut = _consoleManager.ReadLine();
            }

            return stringOut;
        }
    }
}
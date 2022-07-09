using CourseJournal.TrainerApp.Client;
using CourseJournal.TrainerApp.Client.Models;
using System;
using System.Globalization;

namespace CourseJournal.TrainerApp.Client
{
    public interface ICliHelper
    {
        bool GetBool(string message);
        ConsoleColor GetConsoleColor(bool switcher, ConsoleColor defaultColor);
        int GetInt(string message);
        int GetIntInRange(string message, int beginRange, int endRange);
        string GetString(string message);
        DateTime GetValidDateTime(string rangePoint);
        string GetPassword(string message);
        PresenceStatus GetPresenceStatus();
    }

    public class CliHelper : ICliHelper
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

        public int GetIntInRange(string message, int beginRange, int endRange)
        {
            var result = GetInt(message);

            while (result < beginRange || result > endRange)
            {
                _consoleManager.Clear();
                _consoleManager.WriteLine($"(!) Write number from {beginRange} to {endRange}\n");
                result = GetInt(message);
            }

            return result;
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

        public PresenceStatus GetPresenceStatus()
        {
            var functions = string.Join(", ", Enum.GetNames(typeof(PresenceStatus)));
            var input = default(string);
            var output = new PresenceStatus();

            var success = false;
            while (!success)
            {
                while (!Enum.TryParse(input, out output))
                {
                    //_consoleManager.Clear();
                    input = GetString($"Pick presence status [{functions}]");
                }

                if (Enum.IsDefined(typeof(PresenceStatus), input))
                {
                    success = true;
                }
                else
                {
                    _consoleManager.Clear();
                    input = GetString($"(!) Wrong input, try again...\nPick function [{functions}]");
                    continue;
                }
            }

            return output;
        }
    }
}
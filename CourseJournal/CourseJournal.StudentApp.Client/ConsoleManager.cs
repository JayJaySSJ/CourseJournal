﻿using System;

namespace CourseJournal.StudentApp.Client
{
    public interface IConsoleManager
    {
        void Write(string value);
        void WriteLine(string value);
        ConsoleKeyInfo ReadKey();
        string ReadLine();
        void Clear();
    }

    internal class ConsoleManager : IConsoleManager
    {
        public void Clear() => Console.Clear();

        public ConsoleKeyInfo ReadKey() => Console.ReadKey();

        public string ReadLine() => Console.ReadLine();

        public void Write(string value) => Console.Write(value);

        public void WriteLine(string value) => Console.WriteLine(value);
    }
}
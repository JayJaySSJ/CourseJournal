using Microsoft.Owin.Hosting;
using System;

namespace CourseJournal.AdminApp.Server
{
    internal class Program
    {
        const string baseAddress = "http://localhost:5678/";

        static void Main(string[] args)
        {

            using (WebApp.Start<StartUp>(baseAddress))
            {
                Console.WriteLine("Server running...");
                Console.ReadLine();
            }
        }
    }
}
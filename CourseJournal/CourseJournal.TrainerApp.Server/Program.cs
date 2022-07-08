using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseJournal.TrainerApp.Server
{
    internal class Program
    {
        const string baseAddress = "http://localhost:9090";
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

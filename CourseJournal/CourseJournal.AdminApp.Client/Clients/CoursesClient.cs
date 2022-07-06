using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using CourseJournal.AdminApp.Client.Models;
using Newtonsoft.Json;
using System;

namespace CourseJournal.AdminApp.Client.Clients
{
    internal interface ICoursesClient
    {
        Task<bool> CreateNewAsync(Course newCourse);
    }

    internal class CoursesClient : ICoursesClient
    {
        private readonly HttpClient _httpClient;

        private static string _baseUrl => ConfigurationManager.AppSettings["url"];
        private static readonly string _clientPath = _baseUrl + "/api/v1/courses";

        public CoursesClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> CreateNewAsync(Course newCourse)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(newCourse), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _httpClient.PostAsync($@"{_clientPath}", content);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return false;
                }

                return bool.Parse(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
                return false;
            }
        }
    }
}
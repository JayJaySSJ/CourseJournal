using CourseJournal.TrainerApp.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseJournal.TrainerApp.Client.Clients
{
    public interface ICoursesClient
    {
        Task<List<Course>> GetAllAsync();
        Task<bool> AddHomeworkResult(HomeworkResult homeworkResult);
    }

    public class CoursesClient : ICoursesClient
    {
        private readonly HttpClient _httpClient;
        private static string _baseUrl => ConfigurationManager.AppSettings["url"];
        private static readonly string _clientPath = _baseUrl + "/api/v1/courses";

        private readonly IConsoleManager _consoleManager;

        public CoursesClient()
        {
            _httpClient = new HttpClient();
            _consoleManager = new ConsoleManager();
        }

        public async Task<List<Course>> GetAllAsync()
        {
            try
            {
                var responseBody = await _httpClient.GetAsync($@"{_clientPath}");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<Course>();
                }

                return JsonConvert.DeserializeObject<List<Course>>(result);
            }
            catch (HttpRequestException e)
            {
                _consoleManager.WriteLine("\nException Caught!");
                _consoleManager.WriteLine("Message :{0} " + e.Message);
                return new List<Course>();
            }
        }

        public async Task<bool> AddHomeworkResult(HomeworkResult homeworkResult)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(homeworkResult), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _httpClient.PostAsync($@"{_clientPath}/addresulthomework", content);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return false;
                }

                return bool.Parse(result);
            }
            catch (Exception ex)
            {
                _consoleManager.WriteLine("\nException Caught!");
                _consoleManager.WriteLine("Message :{0} " + ex.Message);
                return false;
            }
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using CourseJournal.TrainerApp.Client.Models;

namespace CourseJournal.TrainerApp.Client.Clients
{
    internal interface ITrainersClient
    {
        Task<bool> LoginTrainer(Trainer trainer);
        Task<Trainer> GetTrainerByIdAsync(int id);
    }

    internal class TrainerSClient : ITrainersClient
    {
        private readonly HttpClient _httpClient;
        private static string _baseUrl => ConfigurationManager.AppSettings["url"];
        private static readonly string _clientPath = _baseUrl + "/api/v1/trainers";

        public TrainerSClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> LoginTrainer(Trainer trainer)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(trainer), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _httpClient.PostAsync($@"{_clientPath}/login", content);

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

        public async Task<Trainer> GetTrainerByIdAsync(int id)
        {
            try
            {
                var responseBody = await _httpClient.GetAsync($@"{_clientPath}/{id}");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<Trainer>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new Trainer();
            }
        }
    }
}
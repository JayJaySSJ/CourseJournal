using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using CourseJournal.TrainerApp.Client.Models;
using CourseJournal.AdminApp.Client.Models;

namespace CourseJournal.TrainerApp.Client.Clients
 {
    internal interface ITrainerClient
    {
        Task<bool> LoginTrainer(TrainerModel trainer);
    }

    internal class TrainerClient : ITrainerClient
    {
        private readonly HttpClient _httpClient;
        private static string _baseUrl => ConfigurationManager.AppSettings["url"];
        private static readonly string _clientPath = _baseUrl + "/api/v1/trainer";

        public TrainerClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<bool> LoginTrainer(TrainerModel trainer)
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
    }
}


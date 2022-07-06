using CourseJournal.AdminApp.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client.Clients
{
    public interface ITrainerClient
    {
        Task<bool> CreateTrainer(Trainer trainer);
        Task<bool> CheckIfExists(string email);
    }
    public class TrainerClient : ITrainerClient
    {
        private readonly HttpClient _client;

        private static string _baseUrl => ConfigurationManager.AppSettings["url"];
        private static readonly string _clientPath = _baseUrl + "/api/v1/trainers";

        public TrainerClient()
        {
            _client = new HttpClient();
        }

        public async Task<bool> CreateTrainer(Trainer trainer)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(trainer), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync($@"{_clientPath}", content);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return false;
                }

                return bool.Parse(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return false;
            }
        }

        public async Task<bool> CheckIfExists(string email)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(email), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync($@"{_clientPath}/checkIfExists", content);

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return false;
                }

                return bool.Parse(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return false;
            }
        }

        public async Task<List<Trainer>> GetAllAsync()
        {
            try
            {
                var responseBody = await _client.GetAsync($@"{_clientPath}");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<Trainer>();
                }

                return JsonConvert.DeserializeObject<List<Trainer>>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", ex.Message);
                return new List<Trainer>();
            }
        }

        private async Task<Trainer> GetTrainer(int id)
        {
            try
            {
                var responseBody = await _client.GetAsync($@"{_clientPath}/{id}");

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

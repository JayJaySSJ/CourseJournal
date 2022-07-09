using CourseJournal.AdminApp.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseJournal.AdminApp.Client.Clients
{
    public interface IStudentsWebApiClient
    {
        Task<bool> AddStudentAsync(Student student);

        Task<bool> CheckIfExistEmail(Student student);

        Task<List<Student>> GetAllStudentsAsync();

        Task<Student> GetByIdAsync(int id);
    }

    public class StudentsWebApiClient : IStudentsWebApiClient
    {
        private readonly HttpClient _client;

        private static string _baseUrl => ConfigurationManager.AppSettings["url"];
        private static readonly string _clientPath = _baseUrl + "/api/v1/students";

        public StudentsWebApiClient()
        {
            _client = new HttpClient();
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(student), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync($@"{_clientPath}/create", content);

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

        public async Task<bool> CheckIfExistEmail(Student student)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(student), System.Text.Encoding.UTF8, "application/json");

                var responseBody = await _client.PostAsync($@"{_clientPath}/check", content);

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

        public async Task<Student> GetByIdAsync(int id)
        {
            try
            {
                var responseBody = await _client.GetAsync($@"{_clientPath}/id/{id}");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new Student();
                }

                return JsonConvert.DeserializeObject<Student>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new Student();
            }
        }

      

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            try
            {
                var responseBody = await _client.GetAsync($@"{_clientPath}/getallstudents");

                var result = await responseBody.Content.ReadAsStringAsync();

                if (!responseBody.IsSuccessStatusCode)
                {
                    return new List<Student>();
                }

                return JsonConvert.DeserializeObject<List<Student>>(result);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return new List<Student>();
            }
        }
    }
}
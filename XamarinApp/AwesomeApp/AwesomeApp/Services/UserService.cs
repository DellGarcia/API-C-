using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AwesomeApp.Models;

namespace AwesomeApp.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var response = await _httpClient.GetAsync("user");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<User>>(responseAsString);
        }

        public async Task<User> GetUser(Guid id)
        {
            var response = await _httpClient.GetAsync($"user/{id}");

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(responseAsString);
        }

        public async Task<User> AddUser(User user)
        {
            var response = await _httpClient.PostAsync("user",
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<User>(responseAsString);
        }

        public async Task DeleteUser(User user)
        {
            var response = await _httpClient.DeleteAsync($"user/{user.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task SaveUser(User user)
        {
            Guid? id = user.Id;
            user.Id = null;

            var response = await _httpClient.PutAsync($"user/{id}",
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public bool IsUserFieldsValid(User user)
        {
            if (user.FirstName == string.Empty || user.FirstName == null)
                return false;

            if (user.Age < 13 || user.Age > 200 || user.Age == null)
                return false;

            return true;
        }
    }
}

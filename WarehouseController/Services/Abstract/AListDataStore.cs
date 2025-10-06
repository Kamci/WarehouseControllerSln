using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseController.Services.Implementations.Authorization;

namespace WarehouseController.Services.Abstract
{
    public abstract class AListDataStore<T> : IDataStore<T>
    {
        protected readonly HttpClient _httpClient;
        protected abstract string ControllerName { get; }

        private const string _baseUrl = "https://localhost:7167/api/"; 

       
        protected AListDataStore()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };

            if (!string.IsNullOrEmpty(AuthService.JwtToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthService.JwtToken);
            }
        }

        public virtual async Task<bool> AddItemAsync(T item)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(ControllerName, item);
                var content = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public virtual async Task<bool> UpdateItemAsync(T item)
        {
            try
            {
                int id = GetId(item);
                var response = await _httpClient.PutAsJsonAsync($"{ControllerName}/{id}", item);
                var content = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public virtual async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthService.JwtToken);
                var response = await _httpClient.DeleteAsync($"{ControllerName}/{id}");
                var content = await response.Content.ReadAsStringAsync();
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual async Task<T> GetItemAsync(int id)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<T>($"{ControllerName}/{id}");
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return default!;
            }
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                var response = await _httpClient.GetAsync(ControllerName);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result ?? new List<T>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                return new List<T>();
            }
        }

        protected abstract int GetId(T item);
    
    }
}

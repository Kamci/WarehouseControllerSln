using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WarehouseController.Services.Abstract
{
    public abstract class AListDataStore<T> : IDataStore<T>
    {
        protected readonly HttpClient _httpClient;
        protected abstract string ControllerName { get; }

        private const string _baseUrl = "https://localhost:7167/api/"; // tutaj wpisz adres

        // Konstruktor bezparametrowy
        protected AListDataStore()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl)
            };
        }

        public virtual async Task<bool> AddItemAsync(T item)
        {
            // Dodaj logowanie:
            Debug.WriteLine("Dodaję item do API: " + System.Text.Json.JsonSerializer.Serialize(item));
            var response = await _httpClient.PostAsJsonAsync(ControllerName, item);
            Debug.WriteLine("Status odpowiedzi: " + response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            Debug.WriteLine("Treść odpowiedzi: " + responseBody);
            return response.IsSuccessStatusCode;
        }

        public virtual async Task<bool> UpdateItemAsync(T item)
        {
            int id = GetId(item);
            var response = await _httpClient.PutAsJsonAsync($"{ControllerName}/{id}", item);
            return response.IsSuccessStatusCode;
        }

        public virtual async Task<bool> DeleteItemAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ControllerName}/{id}");
            return response.IsSuccessStatusCode;
        }

        public virtual async Task<T> GetItemAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{ControllerName}/{id}");
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<T>>(ControllerName);
        }

        // Musisz zdefiniować, jak pobierać Id z obiektu T (np. przez refleksję lub interfejs)
        protected abstract int GetId(T item);
    }
}

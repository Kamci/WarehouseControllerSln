using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
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
            try
            {
                Debug.WriteLine($"📤 [Add] {ControllerName}: {JsonSerializer.Serialize(item)}");
                var response = await _httpClient.PostAsJsonAsync(ControllerName, item);
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"📥 [Add] Response: {response.StatusCode} - {content}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ [Add] Error: {ex.Message}");
                return false;
            }
        }

        public virtual async Task<bool> UpdateItemAsync(T item)
        {
            try
            {
                int id = GetId(item);
                Debug.WriteLine($"📤 [Update] {ControllerName}/{id}: {JsonSerializer.Serialize(item)}");
                var response = await _httpClient.PutAsJsonAsync($"{ControllerName}/{id}", item);
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"📥 [Update] Response: {response.StatusCode} - {content}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ [Update] Error: {ex.Message}");
                return false;
            }
        }

        public virtual async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                Debug.WriteLine($"🗑️ [Delete] {ControllerName}/{id}");
                var response = await _httpClient.DeleteAsync($"{ControllerName}/{id}");
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"📥 [Delete] Response: {response.StatusCode} - {content}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ [Delete] Error: {ex.Message}");
                return false;
            }
        }

        public virtual async Task<T> GetItemAsync(int id)
        {
            try
            {
                Debug.WriteLine($"🔎 [GetItem] {ControllerName}/{id}");
                var result = await _httpClient.GetFromJsonAsync<T>($"{ControllerName}/{id}");
                Debug.WriteLine($"✅ [GetItem] Received: {JsonSerializer.Serialize(result)}");
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ [GetItem] Error: {ex.Message}");
                return default!;
            }
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            try
            {
                Debug.WriteLine($"📦 [GetAll] {ControllerName}");
                var response = await _httpClient.GetAsync(ControllerName);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"📥 [GetAll] Response JSON: {json}");

                var result = JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Debug.WriteLine($"✅ [GetAll] Deserialized {result?.Count ?? 0} items");
                return result ?? new List<T>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ [GetAll] Error: {ex.Message}");
                return new List<T>();
            }
        }

        protected abstract int GetId(T item);
    
    }
}

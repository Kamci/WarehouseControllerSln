using System.Collections.ObjectModel;
using System.Net.Http.Json;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Abstract;

namespace WarehouseController.Services
{
    public class OrderDataStore : AListDataStore<OrderDto>
    {
        public OrderDataStore() : base() { }

        protected override string ControllerName => "Order";
        protected override int GetId(OrderDto item) => item.Id ?? 0;
        public async Task<bool> AddOrderFromObjectAsync(object orderToSend)
        {
            var response = await _httpClient.PostAsJsonAsync(ControllerName, orderToSend);
            var responseBody = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine("Status odpowiedzi: " + response.StatusCode);
            System.Diagnostics.Debug.WriteLine("Treść odpowiedzi: " + responseBody);
            return response.IsSuccessStatusCode;
        }
    }
}

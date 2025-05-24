using System.Buffers.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services;
using WarehouseController.Services.Abstract;

public class ProductDataStore : AListDataStore<ProductDto>
{
    public ProductDataStore() : base() { }
    public override async Task<IEnumerable<ProductDto>> GetItemsAsync(bool forceRefresh = false)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{ControllerName}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<List<ProductDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            return result ?? new List<ProductDto>();
        }
        catch (Exception ex)
        {
            return new List<ProductDto>();
        }
    }
    protected override string ControllerName => "Product";
    protected override int GetId(ProductDto item) => item.Id;

}

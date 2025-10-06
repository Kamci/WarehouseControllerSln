using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;

namespace WarehouseController.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<List<ProductDto>> GetTopProductsAsync(int warehouseId);
        Task<int> GetProductsCountAsync(int warehouseId);
        Task<int> GetLowStockItemCountAsync(int warehouseId);
    }
}

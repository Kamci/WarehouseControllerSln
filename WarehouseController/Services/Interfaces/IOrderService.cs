using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;

namespace WarehouseController.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        public Task<List<OrderDto>> GetRecentOrdersAsync(int warehouseId);
        public Task<int> GetOpenOrdersCountAsync(int warehouseId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.Services.Interfaces
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Warehouse>> GetAllAsync();
        Task<Warehouse> GetByIdAsync(int id);
    }
}

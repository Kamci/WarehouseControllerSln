using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.Services.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>>GetAllAsync();
        Task<Supplier> GetByIdAsync(int id);
    }
}

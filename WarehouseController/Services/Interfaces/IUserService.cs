using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.Services.Interfaces
{
    public interface IUserService
    {

        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
    }
}

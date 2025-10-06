using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services.DataStores;
using WarehouseController.Services.Interfaces;

namespace WarehouseController.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserDataStore _dataStore;

        public UserService()
        {
            _dataStore = new UserDataStore(); 
        }

        public Task<IEnumerable<User>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<User> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);
    }
}

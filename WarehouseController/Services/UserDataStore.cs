using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.Services
{
    public class UserDataStore : IDataStore<User>
    {
        private readonly ObservableCollection<User> users = new()
    {
        new User
        {
            Id = 1,
            Login = "admin",
            PasswordHash = Encoding.UTF8.GetBytes("hashedPassword1"),
            Role = "Administrator"
        },
        new User
        {
            Id = 2,
            Login = "john.doe",
            PasswordHash = Encoding.UTF8.GetBytes("hashedPassword2"),
            Role = "User"
        },
        new User
        {
            Id = 3,
            Login = "jane.smith",
            PasswordHash = Encoding.UTF8.GetBytes("hashedPassword3"),
            Role = "Manager"
        }
    };


        public async Task<bool> AddItemAsync(User item)
        {
            users.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = users.FirstOrDefault(u => u.Id == id);
            if (oldItem != null)
                users.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(int id)
        {
            return await Task.FromResult(users.FirstOrDefault(u => u.Id == id));
        }

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult<IEnumerable<User>>(users);
        }

        public async Task<bool> UpdateItemAsync(User item)
        {
            var oldItem = users.FirstOrDefault(u => u.Id == item.Id);
            if (oldItem != null)
            {
                users.Remove(oldItem);
                users.Add(item);
            }

            return await Task.FromResult(true);
        }
    }
}

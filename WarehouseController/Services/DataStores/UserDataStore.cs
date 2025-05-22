using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services.Abstract;

namespace WarehouseController.Services.DataStores
{
    public class UserDataStore : AListDataStore<User>
    {
        public UserDataStore() : base() { }

        protected override string ControllerName => "User";
        protected override int GetId(User item) => item.Id;
    }
}
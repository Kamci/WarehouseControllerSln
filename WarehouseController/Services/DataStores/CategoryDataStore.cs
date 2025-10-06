using System.Net.Http;
using System.Net.Http.Json;
using WarehouseController.Model;
using WarehouseController.Services.Abstract;

namespace WarehouseController.Services.DataStores
{
    public class CategoryDataStore : AListDataStore<Category>
    {
        public CategoryDataStore() : base() { }

        protected override string ControllerName => "Category";
        protected override int GetId(Category item) => item.Id;
    }

}
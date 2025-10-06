using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.CategoryVM
{
    public partial class AddCategoryViewModel : ANewItemViewModel<Category>
    {
        private int id;
        private string name = string.Empty;

        public AddCategoryViewModel() : base("Add Category")
        {  }


        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }



        public override bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(name);
        }

        public override Category SetItem()
            => new Category()
            {
                Name = Name
            };
    }
}

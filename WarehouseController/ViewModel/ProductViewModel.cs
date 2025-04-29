using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.ViewModel.Abstract;
using WarehouseController.Model;
using System.Windows.Input;
using WarehouseController.View.ProductView;

namespace WarehouseController.ViewModel
{
    public class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        {
            Title = "Product";
            Products = new List<Product>();
            AddProductCommand = new Command(async () => await GoToAddProductPage());
        }

        internal List<Product> Products { get; private set; }
        public ICommand AddProductCommand { get; }
        private async Task GoToAddProductPage()
        {
            // Załaduj nową stronę przez Shell
            await Shell.Current.GoToAsync(nameof(AddProductPage));
        }
    }
}

using WarehouseController.ViewModel.Abstract;
using WarehouseController.Model;
using WarehouseController.View.ProductView;
namespace WarehouseController.ViewModel.ProductVM
{
    public class ProductViewModel : AItemListViewModel<Product>
    {
        private Product? _selectedProduct;
        public Command RefreshCommand { get; }
        public Command<Product> AboutCommand { get; }
        public ProductViewModel():base("Product") 
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<Product>(OnProductSelected);
        }

      

        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }
        async void OnProductSelected(Product product)
        {
            await GoToDetailsPage(product);
        }

        public override async Task GoToAddPage()
        {
            // Załaduj nową stronę przez Shell
            await Shell.Current.GoToAsync(nameof(AddProductPage));
        }

        public override async Task GoToDetailsPage(Product product)
        {
            if (product == null)
                return;
            SelectedProduct = product;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DetailProductPage)}?{nameof(DetailProductViewModel.ItemId)}={product.Id}");
        }
    }
}

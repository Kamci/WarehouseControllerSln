using WarehouseController.ViewModel.Abstract;
using WarehouseController.Model;
using WarehouseController.View.ProductView;
using WarehouseController.DTO;
namespace WarehouseController.ViewModel.ProductVM
{
    public partial class ProductViewModel : AItemListViewModel<ProductDto>
    {
        private ProductDto? _selectedProduct;
        public Command RefreshCommand { get; }
        public Command<ProductDto> AboutCommand { get; }
        public ProductViewModel():base("Product") 
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<ProductDto>(OnProductSelected);
        }

      

        public ProductDto? SelectedProduct
        {
            get => _selectedProduct;
            set => SetProperty(ref _selectedProduct, value);
        }
        async void OnProductSelected(ProductDto product)
        {
            await GoToDetailsPage(product);
        }

        public override async Task GoToAddPage()
        {
           
            await Shell.Current.GoToAsync(nameof(AddProductPage));
        }

        public override async Task GoToDetailsPage(ProductDto product)
        {
            if (product == null)
                return;
            SelectedProduct = product;
           
            await Shell.Current.GoToAsync($"{nameof(DetailProductPage)}?{nameof(DetailProductViewModel.ItemId)}={product.Id}");
        }
    }
}

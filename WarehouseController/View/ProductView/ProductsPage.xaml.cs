using WarehouseController.ViewModel.ProductVM;
using WarehouseController.Model;
using WarehouseController.DTO;
namespace WarehouseController;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ProductsPage : ContentPage
{
    ProductViewModel _viewModel;
    public ProductsPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new ProductViewModel();
       
    }

    private async void ProductsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ProductsList.SelectedItem is ProductDto selectedProduct)
        {
            bool isConfirmed = await DisplayAlert("Product Selected", $"You selected: {selectedProduct.Name}", "OK", "Cancel");

            if (isConfirmed)
            {
                _viewModel.SelectedProduct = selectedProduct;
            }
            else
            {
                ProductsList.SelectedItem = null;
            }

        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ProductViewModel vm)
            vm.LoadItemsCommand.Execute(null);
        _viewModel.OnAppearing();
    }


}
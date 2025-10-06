using WarehouseController.ViewModel.ProductVM;
using WarehouseController.Model;

namespace WarehouseController.View.ProductView;

public partial class EditProductPage : ContentPage
{
	public EditProductPage()
	{
        InitializeComponent();
        BindingContext = new EditProductViewModel();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is EditProductViewModel vm)
        {
            await vm.LoadDataAsync();
        }
    }

}
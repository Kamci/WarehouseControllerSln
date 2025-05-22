
using WarehouseController.ViewModel.ProductVM;

namespace WarehouseController.View.ProductView;

public partial class DetailProductPage : ContentPage
{
    public DetailProductPage()
    {
        InitializeComponent();
        BindingContext = new DetailProductViewModel();
    }
}
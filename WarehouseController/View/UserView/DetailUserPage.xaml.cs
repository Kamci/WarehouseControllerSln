using WarehouseController.ViewModel.ProductVM;
using WarehouseController.ViewModel.UserVM;

namespace WarehouseController.View.UserView;

public partial class DetailUserPage : ContentPage
{
	public DetailUserPage()
	{
      
        InitializeComponent();
        BindingContext = new DetailUserViewModel();
    }
}
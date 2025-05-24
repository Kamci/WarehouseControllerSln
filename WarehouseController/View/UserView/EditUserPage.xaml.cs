using System.Diagnostics;
using WarehouseController.ViewModel.UserVM;

namespace WarehouseController.View.UserView;

public partial class EditUserPage : ContentPage
{
	public EditUserPage()
	{
        BindingContext = new EditUserViewModel(); 
        InitializeComponent();

	}
   
}
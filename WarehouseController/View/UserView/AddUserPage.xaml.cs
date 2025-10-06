using System.Diagnostics;
using WarehouseController.Model;
using WarehouseController.ViewModel.UserVM;
namespace WarehouseController.View.UserView;

public partial class AddUserPage : ContentPage
{
    public AddUserPage()
    {
        BindingContext = new AddUserViewModel();
        InitializeComponent();
    }

}
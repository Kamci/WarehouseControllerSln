using System.Diagnostics;
using WarehouseController.Model;
using WarehouseController.ViewModel.UserVM;
namespace WarehouseController.View.UserView;

public partial class AddUserPage : ContentPage
{
    public AddUserPage()
    {
        BindingContext = new AddUserViewModel(); // przypisz ViewModel
        InitializeComponent();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Debug.WriteLine("AddUserPage is disappearing");
    }
}
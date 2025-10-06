using WarehouseController.Model;
using WarehouseController.ViewModel.UserVM;
namespace WarehouseController;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class UserPage : ContentPage
{
    UserViewModel _viewModel;
    public UserPage()
    {
        InitializeComponent(); 
        BindingContext = _viewModel = new UserViewModel();
    }


 

    private async void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (UserList.SelectedItem is User selectedUser)
        {
            bool isConfirmed = await DisplayAlert("User Selected", $"You selected: {selectedUser.Login}", "OK", "Cancel");

            if (isConfirmed)
            {
                _viewModel.SelectedUser = selectedUser;
            }
            else
            {
                UserList.SelectedItem = null;
            }

        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is UserViewModel vm)
            vm.LoadItemsCommand.Execute(null);
        _viewModel.OnAppearing();
    }

}
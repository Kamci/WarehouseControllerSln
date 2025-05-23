namespace WarehouseController.View;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Tu później dodamy logikę
        await Shell.Current.GoToAsync("//DashBoard");
    }
}
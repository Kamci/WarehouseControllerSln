using WarehouseController.Services.Implementations.Authorization;

namespace WarehouseController.View;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;

        var login = EntryLogin.Text?.Trim();
        var password = EntryPassword.Text;

        if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Enter Login and Password.", "OK");
            return;
        }

        var authService = new AuthService();
        var success = await authService.LoginAsync(login, password);

        if (success)
        {
            await Shell.Current.GoToAsync("//DashBoard");
        }
        else
        {
            await DisplayAlert("Error", "Incorrect login details", "OK");
        }
      
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
    }
    private async void OnGuestInfoClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Guest Login", "Username: guest\nPassword: guest", "OK");
    }
}
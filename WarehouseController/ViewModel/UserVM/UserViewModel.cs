using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.ViewModel.Abstract;
using WarehouseController.Model;
using WarehouseController.Services;
using WarehouseController.View.UserView;
using System.Diagnostics;


namespace WarehouseController.ViewModel.UserVM
{
    public class UserViewModel : AItemListViewModel<User>
    {
        private User? _selectedUser;
        public Command RefreshCommand { get; }
        public Command<User> AboutCommand { get; }
        public UserViewModel() : base("User")
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<User>(OnUserSelected);
        }



        public User? SelectedUser
        {
            get => _selectedUser;
            set => SetProperty(ref _selectedUser, value);
        }
        async void OnUserSelected(User user)
        {
            await GoToDetailsPage(user);
        }

        public override async Task GoToAddPage()
        {
            
            await Shell.Current.GoToAsync(nameof(AddUserPage));
        }

        public override async Task GoToDetailsPage(User user)
        {
            try
            {
                if (user == null)
                {
                    Debug.WriteLine("User is null. Navigation canceled.");
                    return;
                }

                if (user.Id <= 0)
                {
                    Debug.WriteLine($"Invalid user ID: {user.Id}. Navigation canceled.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(user.Login))
                {
                    Debug.WriteLine("User login is null or empty. Navigation canceled.");
                    return;
                }

                Debug.WriteLine($"Going to DetailUserPage with ID: {user.Id}");
                Debug.WriteLine($"User: {user.Login}");
                Debug.WriteLine($"User: {user.Role}");

                SelectedUser = user;

                await Shell.Current.GoToAsync($"{nameof(DetailUserPage)}?{nameof(DetailUserViewModel.ItemId)}={user.Id}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERROR] {ex}");
            }
        }
    }
}

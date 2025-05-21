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
            // Załaduj nową stronę przez Shell
            await Shell.Current.GoToAsync(nameof(AddUserPage));
        }

        public override async Task GoToDetailsPage(User user)
        {
            try 
            {
                Debug.WriteLine($"[NAVIGATE] Going to DetailUserPage with ID: {user.Id}");
                Debug.WriteLine($"[NAVIGATE] User: {user.Login}");
                Debug.WriteLine($"[NAVIGATE] User: {user.Role}");
                if (user == null)
                    return;
                SelectedUser = user;

                //// This will push the ItemDetailPage onto the navigation stack
                await Shell.Current.GoToAsync($"{nameof(DetailUserPage)}?{nameof(DetailUserViewModel.ItemId)}={user.Id}");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[NAVIGATE ERROR] {ex}");
            }
           
        }
    }
}

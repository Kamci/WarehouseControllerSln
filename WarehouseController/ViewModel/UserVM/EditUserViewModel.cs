using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services.Implementations;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.UserVM
{

    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class EditUserViewModel : AItemUpdateViewModel<User>
    {
        private int id;
        private string login;
        private string role;

        public EditUserViewModel() : base("Edit User")
        {
           
        }

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Login { get => login; set => SetProperty(ref login, value); }
        public string Role { get => role; set => SetProperty(ref role, value); }

        #region Password logic
        private string password;
        public string Password
        {
            get => password;
            set
            {
               
                SetProperty(ref password, value);
            }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
               
                SetProperty(ref repeatPassword, value);
            }
        }

        public bool PasswordsMatch => Password == RepeatPassword;
        #endregion

        public override bool ValidateSave()
        {
            bool passwordValid = string.IsNullOrWhiteSpace(Password) || PasswordsMatch;

            bool result = !string.IsNullOrWhiteSpace(Login)
                       && !string.IsNullOrWhiteSpace(Role)
                       && passwordValid;

            
            return result;
        }

        public override User SetItem()
        {

            return new User()
            {
                Id = Id,
                Login = Login,
                Role = Role,
                Password = Password  // aktualne lub zaktualizowane
            };
        }

        public override async Task LoadItem(int id)
        {
            var user = await DataStore.GetItemAsync(id);
            if (user == null)
            {
              
                return;
            }

            Id = user.Id;
            Login = user.Login;
            Role = user.Role;

            Password = string.Empty; // dla UI
            RepeatPassword = string.Empty;
        }
    }
}

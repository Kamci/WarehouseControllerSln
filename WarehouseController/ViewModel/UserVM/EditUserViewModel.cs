using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.UserVM
{
    public class EditUserViewModel : AItemUpdateViewModel<User>
    {
        private int id;
        private string login;
        private byte[] passwordHash;
        private string role;

        public EditUserViewModel() : base("Edit User")
        {
            Debug.WriteLine("EditUserViewModel initialized.");
        }

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Login { get => login; set => SetProperty(ref login, value); }
        public byte[] PasswordHash { get => passwordHash; set => SetProperty(ref passwordHash, value); }
        public string Role { get => role; set => SetProperty(ref role, value); }

        #region Password logic
        private string password;
        public string Password
        {
            get => password;
            set
            {
                Debug.WriteLine($"[EditUserViewModel] Setting new Password (plaintext): {value}");
                SetProperty(ref password, value);

                if (string.IsNullOrWhiteSpace(value))
                {
                    Debug.WriteLine("[EditUserViewModel] Password left unchanged.");
                    return;
                }

                try
                {
                    PasswordHash = PasswordHelper.HashPassword(value);
                    Debug.WriteLine($"[EditUserViewModel] New hash set (Base64): {Convert.ToBase64String(PasswordHash)}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[EditUserViewModel] Hashing failed: {ex.Message}");
                }
            }
        }

        private string repeatPassword;
        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
                Debug.WriteLine($"[EditUserViewModel] Setting RepeatPassword (plaintext): {value}");
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

            Debug.WriteLine($"[EditUserViewModel] ValidateSave result: {result}");
            return result;
        }

        public override User SetItem()
        {
            Debug.WriteLine($"[EditUserViewModel] Preparing updated user: {Login}");

            return new User()
            {
                Id = Id,
                Login = Login,
                Role = Role,
                PasswordHash = PasswordHash // aktualne lub zaktualizowane
            };
        }

        public override async Task LoadItem(int id)
        {
            var user = await DataStore.GetItemAsync(id);
            if (user == null)
            {
                Debug.WriteLine($"[EditUserViewModel] User with ID {id} not found.");
                return;
            }

            Id = user.Id;
            Login = user.Login;
            Role = user.Role;
            PasswordHash = user.PasswordHash;

            Password = string.Empty; // dla UI
            RepeatPassword = string.Empty;
        }
    }
}

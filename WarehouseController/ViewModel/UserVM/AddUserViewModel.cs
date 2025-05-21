using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.ViewModel.Abstract;
using WarehouseController.Model;
using System.Diagnostics;
using WarehouseController.Services;

namespace WarehouseController.ViewModel.UserVM
{
    class AddUserViewModel : ANewItemViewModel<User>
    {
        private int id;
        private string login = string.Empty;
        private byte[] passwordHash;
        private string role = string.Empty;
        public AddUserViewModel() : base("Add User")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("AddUserViewModel loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel init error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }
        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Login { get => login; set => SetProperty(ref login, value); }
        public byte[] PasswordHash { get => passwordHash; set => SetProperty(ref passwordHash, value); }
        public string Role { get => role; set => SetProperty(ref role, value); }
        public override bool ValidateSave()
        {
            bool result = !string.IsNullOrWhiteSpace(login)
           && !string.IsNullOrWhiteSpace(role)
           && PasswordHash != null
           && PasswordsMatch;

            Debug.WriteLine($"[ViewModel] ValidateSave result: {result}");
            return result;
        }
        public override User SetItem()
        {
            Debug.WriteLine($"[ViewModel] Creating User object with login: {Login}, role: {Role}");
            return new User()
            {
                Id = Id,
                Login = Login,
                PasswordHash = PasswordHash,
                Role = Role
            };
        }

        #region Password logic
        private string password;
        public string Password
        {
            get => password;
            set
            {
                Debug.WriteLine($"[ViewModel] Setting Password (plaintext): {value}");
                SetProperty(ref password, value);

                if (string.IsNullOrWhiteSpace(value))
                {
                    PasswordHash = null;
                    Debug.WriteLine("[ViewModel] Password is empty — hash not set.");
                    return;
                }

                try
                {
                    PasswordHash = PasswordHelper.HashPassword(value);
                    Debug.WriteLine($"[ViewModel] Password hash set (Base64): {Convert.ToBase64String(PasswordHash)}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"[ViewModel] Error hashing password: {ex.Message}");
                }
            }
        }
        public bool VerifyPassword(string inputPassword)
        {
            bool result = PasswordHelper.VerifyPassword(inputPassword, PasswordHash);
            Debug.WriteLine($"[ViewModel] Password verification result: {result}");
            return result;
        }

        public string PasswordHashString =>
        PasswordHash != null ? Convert.ToBase64String(PasswordHash) : string.Empty;

        public void Clear()
        {
            Id = 0;
            Login = string.Empty;
            Role = string.Empty;

            // nie używaj settera
            password = string.Empty;
            PasswordHash = null;

            // tylko powiadom UI, ale nie triggeruj hashowania
            OnPropertyChanged(nameof(Password));
        }
        private string repeatPassword;
        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
                Debug.WriteLine($"[ViewModel] Setting RepeatPassword (plaintext): {value}");
                SetProperty(ref repeatPassword, value);
            }
        }

        public bool PasswordsMatch => Password == RepeatPassword;
        #endregion
    }
}



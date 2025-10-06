using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.UserVM
{
   public partial  class AddUserViewModel : ANewItemViewModel<User>
    {
        private int id;
        private string login = string.Empty;
        private string role = string.Empty;


        public AddUserViewModel() : base("Add User")
        {}
        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Login { get => login; set => SetProperty(ref login, value); }
        public string Role { get => role; set => SetProperty(ref role, value); }
        public override bool ValidateSave()
        {
            bool result = !string.IsNullOrWhiteSpace(login)
           && !string.IsNullOrWhiteSpace(role)
           && Password != null
           && PasswordsMatch;

            return result;
        }
        public override User SetItem()
        {
            return new User()
            {
                Login = Login,
                Password = Password,
                Role = Role,
            };
          
        }

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



        public void Clear()
        {
            Id = 0;
            Login = string.Empty;
            Role = string.Empty;
            password = string.Empty;
            repeatPassword = string.Empty;

            OnPropertyChanged(nameof(Password));
            OnPropertyChanged(nameof(RepeatPassword));
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
    }
}



using System.Diagnostics;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.UserView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.UserVM
{
    public class DetailUserViewModel : AItemDetailsViewModel<User>
    {
        private int id;
        private string login;
        private string role;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Login { get => login; set => SetProperty(ref login, value); }
        public string Role { get => role; set => SetProperty(ref role, value); }

        public DetailUserViewModel() : base("User Details") 
        {
           
        }

        public override async Task LoadItem(int id)
        {
            try
            {
             
              
                var user = await DataStore.GetItemAsync(id);
                if (user != null)
                {
                    Id = user.Id;
                    Login = user.Login;
                    Role = user.Role;
                }
                else
                {
                    Debug.WriteLine($"User with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading user: {ex}");
                throw; 
            }
            
        }

        protected override async Task GoToUpdatePage()
        {
           
            await Shell.Current.GoToAsync($"{nameof(EditUserPage)}?{nameof(EditUserViewModel.ItemId)}={Id}");
        }
    }
}
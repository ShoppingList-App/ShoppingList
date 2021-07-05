using ShoppingListApp.Views;
using System.Diagnostics;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    public class LoginViewModel : BaseShoppingListViewModel
    {
        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

            if (Application.Current.Properties.ContainsKey("username") && Application.Current.Properties.ContainsKey("password"))
            {
                Username = Application.Current.Properties["username"].ToString();
                Password = Application.Current.Properties["password"].ToString();
                _ = Shell.Current.GoToAsync($"//{nameof(ShoppingListsPage)}");
            }
        }

        public string Username
        {
            set
            {
                Application.Current.Properties["username"] = value;
                IO.Swagger.Client.Configuration.Username = value;
            }
        }

        public string Password
        {
            set
            {
                Application.Current.Properties["password"] = value;
                IO.Swagger.Client.Configuration.Password = value;
            }
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Application.Current.SavePropertiesAsync();
            await Shell.Current.GoToAsync($"//{nameof(ShoppingListsPage)}");
        }
    }
}

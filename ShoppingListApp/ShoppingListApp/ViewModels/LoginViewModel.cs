using ShoppingListApp.Views;
using System;
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

            if (Application.Current.Properties.ContainsKey("host")
                && Application.Current.Properties.ContainsKey("username")
                && Application.Current.Properties.ContainsKey("password"))
            {
                Host = Application.Current.Properties["host"].ToString();
                Username = Application.Current.Properties["username"].ToString();
                Password = Application.Current.Properties["password"].ToString();
                _ = Shell.Current.GoToAsync($"//{nameof(ShoppingListsPage)}");
            }
        }

        public string Host
        {
            set
            {
                Application.Current.Properties["host"] = value;
                string url = $"https://{value}/v1";
                IO.Swagger.Client.Configuration.DefaultApiClient = new IO.Swagger.Client.ApiClient(url);
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
            await Application.Current.SavePropertiesAsync();
            await Shell.Current.GoToAsync($"//{nameof(ShoppingListsPage)}");
        }
    }
}

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

            if (CheckConfiguration())
            {
                try
                {
                    SetConfiguration();
                    _ = Shell.Current.GoToAsync($"//{nameof(ShoppingListsPage)}");
                } catch (Exception)
                {
                    // nix
                }
            }
        }

        public string Host
        {
            set
            {
                Application.Current.Properties["host"] = value;
            }
        }

        public string Username
        {
            set
            {
                Application.Current.Properties["username"] = value;
            }
        }

        public string Password
        {
            set
            {
                Application.Current.Properties["password"] = value;
            }
        }

        private void SetConfiguration()
        {
            IO.Swagger.Client.Configuration.DefaultApiClient = new IO.Swagger.Client.ApiClient($"https://{Application.Current.Properties["host"]}/v1");
            IO.Swagger.Client.Configuration.Username = Application.Current.Properties["username"].ToString();
            IO.Swagger.Client.Configuration.Password = Application.Current.Properties["password"].ToString();
        }

        private bool CheckConfiguration()
        {
            return Application.Current.Properties.ContainsKey("host")
                && Application.Current.Properties.ContainsKey("username")
                && Application.Current.Properties.ContainsKey("password");
        }

        private async void OnLoginClicked(object obj)
        {
            try
            {
                if (CheckConfiguration())
                {
                    await Application.Current.SavePropertiesAsync();
                    SetConfiguration();
                    await Shell.Current.GoToAsync($"//{nameof(ShoppingListsPage)}");
                }
            } catch (Exception)
            {
                // nix
            }
        }
    }
}

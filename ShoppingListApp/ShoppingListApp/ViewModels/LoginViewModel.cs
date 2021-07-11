using Newtonsoft.Json;
using ShoppingListApp.Models;
using ShoppingListApp.Views;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using ZXing;

namespace ShoppingListApp.ViewModels
{
    [QueryProperty("Barcode", "Barcode")]
    public class LoginViewModel : BaseShoppingListViewModel
    {
        public Command LoginCommand { get; }
        public Command<Result> ScanCommand { get; }


        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);

            if (CheckConfiguration())
            {
                try
                {
                    SetConfiguration();
                    _ = Shell.Current.GoToAsync($"//{nameof(ShoppingListsPage)}");
                }
                catch { }
            }
        }

        public string Host
        {
            set
            {
                if (Uri.TryCreate(value, UriKind.Absolute, out _))
                {
                    Application.Current.Properties["host"] = value;
                } else
                {
                    Application.Current.Properties["host"] = "";
                }
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

        public string Barcode { get; set; }

        private void SetConfiguration()
        {
            IO.Swagger.Client.Configuration.DefaultApiClient = new IO.Swagger.Client.ApiClient(Application.Current.Properties["host"].ToString());
            IO.Swagger.Client.Configuration.Username = Application.Current.Properties["username"].ToString();
            IO.Swagger.Client.Configuration.Password = Application.Current.Properties["password"].ToString();

            ShoppingListDataStore.LoginUpdate();
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
            }
            catch { }
        }
    }
}

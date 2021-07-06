using ShoppingListApp.Views;
using System;
using Xamarin.Forms;

namespace ShoppingListApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute($"{nameof(ShoppingListsPage)}/{nameof(NewShoppingListPage)}", typeof(NewShoppingListPage));
            Routing.RegisterRoute($"{nameof(ShoppingListsPage)}/{nameof(ShoppingListPage)}", typeof(ShoppingListPage));
            Routing.RegisterRoute($"{nameof(ShoppingListsPage)}/{nameof(ShoppingListPage)}/{nameof(NewShoppingItemPage)}", typeof(NewShoppingItemPage));

            CurrentItem = loginItem;
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            IO.Swagger.Client.Configuration.DefaultApiClient = new IO.Swagger.Client.ApiClient();
            IO.Swagger.Client.Configuration.Username = null;
            IO.Swagger.Client.Configuration.Password = null;

            Application.Current.Properties.Remove("host");
            Application.Current.Properties.Remove("username");
            Application.Current.Properties.Remove("password");
            await Application.Current.SavePropertiesAsync();

            await Current.GoToAsync("//LoginPage");
        }
    }
}

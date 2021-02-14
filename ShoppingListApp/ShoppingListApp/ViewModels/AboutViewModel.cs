using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    public class AboutViewModel : BaseShoppingListViewModel
    {
        public Command DeleteDatabaseCommand { get; }

        public AboutViewModel()
        {
            Title = "About";
            DeleteDatabaseCommand = new Command(DeleteDatabase);
        }

        private async void DeleteDatabase()
        {
            await ShoppingListDataStore.ResetDatabaseAsync();
        }
    }
}
using ShoppingListApp.Models;
using ShoppingListApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    class ShoppingListsViewModel : BaseShoppingListViewModel
    {
        public ObservableCollection<ShoppingList> ShoppingLists { get; }

        public Command LoadShoppingListsCommand { get; }
        public Command AddShoppingListCommand { get; }
        public Command<ShoppingList> ShoppingListTapped { get; }
        public Command<ShoppingList> RemoveShoppingList { get; }

        public ShoppingListsViewModel()
        {
            Title = "Shopping Lists";
            ShoppingLists = new ObservableCollection<ShoppingList>();

            LoadShoppingListsCommand = new Command(async () => await ExecuteLoadShoppingListsCommand());
            ShoppingListTapped = new Command<ShoppingList>(OnShoppingListSelected);
            RemoveShoppingList = new Command<ShoppingList>(OnShoppingListRemove);
            AddShoppingListCommand = new Command(OnAddShoppingList);
        }

        private async Task ExecuteLoadShoppingListsCommand()
        {
            IsBusy = true;

            try
            {
                ShoppingLists.Clear();
                System.Collections.Generic.IEnumerable<ShoppingList> items = await ShoppingListDataStore.GetShoppingListsAsync();
                foreach (ShoppingList item in items)
                {
                    ShoppingLists.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async void OnAddShoppingList()
        {
            await Shell.Current.GoToAsync(nameof(NewShoppingListPage));
        }

        private async void OnShoppingListSelected(ShoppingList shoppingList)
        {
            if (shoppingList != null)
            {
                // Das ist eine URL. ? ist der Beginn der Parameter
                await Shell.Current.GoToAsync($"{nameof(ShoppingListPage)}?{nameof(ShoppingListViewModel.ShoppingListId)}={shoppingList.Id}");
            }
        }

        private async void OnShoppingListRemove(ShoppingList shoppingList)
        {
            if (shoppingList != null)
            {
                _ = await ShoppingListDataStore.RemoveShoppingListAsync(shoppingList.Id);
                IsBusy = true;
            }
        }
    }
}

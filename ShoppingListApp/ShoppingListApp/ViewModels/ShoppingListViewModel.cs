using ShoppingListApp.Models;
using ShoppingListApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    [QueryProperty("ShoppingListId", "ShoppingListId")]
    class ShoppingListViewModel : BaseShoppingListViewModel
    {
        public ObservableCollection<ShoppingItem> ShoppingItems { get; }

        private string shoppingListId;
        private string text;
        public string Id { get; set; }

        public Command LoadShoppingItemsCommand { get; }
        public Command AddShoppingItemCommand { get; }
        public Command RemoveShoppingItemCommand { get; }

        public ShoppingListViewModel()
        {
            ShoppingItems = new ObservableCollection<ShoppingItem>();

            AddShoppingItemCommand = new Command(OnAddShoppingItem);
            LoadShoppingItemsCommand = new Command(async () => await ExecuteLoadShoppingItemsCommand());
            RemoveShoppingItemCommand = new Command(OnRemoveShoppingItem);
        }

        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedItem = null;
        }

        private async Task ExecuteLoadShoppingItemsCommand()
        {
            IsBusy = true;

            try
            {
                ShoppingItems.Clear();
                System.Collections.Generic.IEnumerable<ShoppingItem> items = await DataStore.GetShoppingItemsAsync(shoppingListId);
                foreach (ShoppingItem item in items)
                {
                    ShoppingItems.Add(item);
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

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string ShoppingListId
        {
            get
            {
                return shoppingListId;
            }
            set
            {
                shoppingListId = value;
                LoadShoppingList(value);
            }
        }

        public async void LoadShoppingList(string shoppingListId)
        {
            try
            {
                ShoppingList item = await DataStore.GetShoppingListAsync(shoppingListId);
                Id = item.Id;
                Text = item.Text;
                Title = item.Text;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        public async void OnAddShoppingItem()
        {
            await Shell.Current.GoToAsync($"{nameof(NewShoppingItemPage)}?{nameof(NewShoppingItemViewModel.ShoppingListId)}={ShoppingListId}");
        }

        public async void OnRemoveShoppingItem(object shoppingItemId)
        {
            if (shoppingItemId is string @sii)
            {
                ShoppingItem shoppingItem = await DataStore.RemoveShoppingListItemAsync(ShoppingListId, @sii);
                _ = ShoppingItems.Remove(shoppingItem);
            }
        }
    }
}

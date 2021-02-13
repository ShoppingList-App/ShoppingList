using ShoppingListApp.Models;
using ShoppingListApp.Views;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    [QueryProperty("SelectedMode", "SelectedMode"), QueryProperty("ShoppingListId", "ShoppingListId")]
    class ShoppingListViewModel : BaseShoppingListViewModel
    {
        public enum Mode
        {
            Modify,
            Shop
        }

        public ObservableCollection<ShoppingItem> ShoppingItems { get; }

        private string shoppingListId;
        private Mode selectedMode;
        private string text;
        private uint lastRemovedSortKey = 0;
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
                System.Collections.Generic.IEnumerable<ShoppingItem> items;
                if (selectedMode == Mode.Modify)
                {
                    items = await ShoppingListDataStore.GetShoppingItemsAsync(shoppingListId);
                } else
                {
                    items = await ShoppingListDataStore.GetShoppingItemsOrderBySortKeyAsync(shoppingListId);
                }
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

        public string SelectedMode
        {
            get => selectedMode.ToString();
            set => selectedMode = (Mode)Enum.Parse(typeof(Mode), value);
        }

        public async void LoadShoppingList(string shoppingListId)
        {
            try
            {
                ShoppingList item = await ShoppingListDataStore.GetShoppingListAsync(shoppingListId);
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
                ShoppingItem shoppingItem = await ShoppingListDataStore.RemoveShoppingListItemAsync(ShoppingListId, @sii);
                _ = ShoppingItems.Remove(shoppingItem);
                StoreItem storeItem = await ShoppingListDataStore.GetStoreItemAsync(shoppingItem.StoreItemId);

                if (selectedMode == Mode.Shop)
                {
                    // store item was never shopped
                    // or store item has wrong position in list
                    if (storeItem.SortKey == 0 || storeItem.SortKey <= lastRemovedSortKey)
                    {
                        storeItem.SortKey = lastRemovedSortKey + 1;
                        _ = await ShoppingListDataStore.UpdateStoreItemSortKeyAsync(storeItem.Id, storeItem.SortKey);
                    }

                    lastRemovedSortKey = storeItem.SortKey;
                }
            }
        }
    }
}

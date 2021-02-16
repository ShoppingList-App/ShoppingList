using ShoppingListApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    [QueryProperty("ShoppingListId", "ShoppingListId")]
    class NewShoppingItemViewModel : BaseShoppingListViewModel
    {
        private StoreItem selectedStoreItem;
        private string text;
        private uint amount = 1;
        private string unit;

        public ObservableCollection<StoreItem> StoreItems { get; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewShoppingItemViewModel()
        {
            Title = "New Shopping Item";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            // erzeugt eine neue Funktion die zu PropertyChangedEventHandler passt und ruft darun SaveCommand.ChangeCanExecute auf => WTF????
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();

            unit = StaticValues.Units.First();

            StoreItems = new ObservableCollection<StoreItem>();
            LoadStoreItems();
        }

        private void LoadStoreItems()
        {
            StoreItems.Clear();
            IEnumerable<StoreItem> items = ShoppingListDataStore.GetStoreItemsAsync().Result;
            foreach (StoreItem item in items)
            {
                StoreItems.Add(item);
            }
        }

        public StoreItem SelectedStoreItem
        {
            get => selectedStoreItem;
            set
            {
                selectedStoreItem = value;
                Text = value.Text;
                Unit = value.Unit;
            }
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public uint Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string Unit
        {
            get => unit;
            set => SetProperty(ref unit, value);
        }

        public int ShoppingListId { get; set; }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Text)
                && Amount > 0
                && !string.IsNullOrWhiteSpace(Unit);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            StoreItem storeItem;
            if (selectedStoreItem == null || selectedStoreItem.Text != Text)
            {
                storeItem = new StoreItem()
                {
                    Text = Text,
                    Unit = Unit
                };
                await ShoppingListDataStore.AddStoreItemAsync(storeItem);
            }
            else
            {
                storeItem = selectedStoreItem;
            }

            ShoppingItem shoppingItem = new ShoppingItem()
            {
                StoreItem = storeItem,
                Amount = Amount,
                Unit = Unit
            };
            await ShoppingListDataStore.AddShoppingItemAsync(ShoppingListId, shoppingItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public async Task<List<StoreItem>> SearchStoreItemsAsync(string searchText)
        {
            IEnumerable<StoreItem> items = await ShoppingListDataStore.SearchStoreItemsAsync(searchText);
            List<StoreItem> ret = new List<StoreItem>();
            ret.AddRange(items);
            return ret;
        }
    }
}

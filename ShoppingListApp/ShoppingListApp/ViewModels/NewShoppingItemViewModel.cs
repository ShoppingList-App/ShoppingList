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
        private uint amount;
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
            System.Collections.Generic.IEnumerable<StoreItem> items = ShoppingListDataStore.GetStoreItemsAsync().Result;
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

        public string ShoppingListId { get; set; }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Text)
                && Amount > 0
                && !string.IsNullOrWhiteSpace(Unit);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"..?{nameof(ShoppingListViewModel.ShoppingListId)}={ShoppingListId}");
        }

        private async void OnSave()
        {
            string storeItemId;
            if (selectedStoreItem != null && selectedStoreItem.Text == Text)
            {
                storeItemId = selectedStoreItem.Id;
            }
            else
            {
                StoreItem storeItem = new StoreItem()
                {
                    Text = Text,
                    Unit = Unit
                };
                storeItemId = await ShoppingListDataStore.AddStoreItemAsync(storeItem);
            }

            ShoppingItem shoppingItem = new ShoppingItem()
            {
                StoreItemId = storeItemId,
                Amount = Amount,
                Unit = Unit
            };
            _ = await ShoppingListDataStore.AddShoppingItemAsync(ShoppingListId, shoppingItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"..?{nameof(ShoppingListViewModel.ShoppingListId)}={ShoppingListId}&{nameof(ShoppingListViewModel.SelectedMode)}=Modify");
        }

        public async Task<List<StoreItem>> SearchStoreItemsAsync(string searchText)
        {
            IEnumerable<StoreItem> items = await ShoppingListDataStore.SearchStoreItemsAsync(searchText);
            List<StoreItem> ret = new List<StoreItem>();
            foreach (StoreItem item in items)
            {
                ret.Add(item);
            }

            return ret;
        }
    }
}

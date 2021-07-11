using ShoppingListApp.Models;
using ShoppingListApp.Views;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    class StoreItemViewModel : BaseShoppingListViewModel
    {
        private const int SEARCH_ITEM_LIMIT = 5;
        private string searchText;
        private StoreItem selectedStoreItem;
        private IEnumerable<StoreItem> searchResult;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command ScanCommand { get; }

        public StoreItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            ScanCommand = new Command(OnScan, ValidateScan);

            // erzeugt eine neue Funktion die zu PropertyChangedEventHandler passt und ruft darun SaveCommand.ChangeCanExecute auf => WTF????
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
            PropertyChanged += (_, __) => ScanCommand.ChangeCanExecute();
        }

        public async Task SearchStoreItemsAsync(string searchText)
        {
            SearchResult = await ShoppingListDataStore.SearchStoreItemsAsync(searchText, null, SEARCH_ITEM_LIMIT);
        }

        public IEnumerable<StoreItem> SearchResult
        {
            get => searchResult;
            set => SetProperty(ref searchResult, value);
        }

        public string SearchText
        {
            get => searchText;
            set => SetProperty(ref searchText, value);
        }

        public StoreItem SelectedStoreItem
        {
            get => selectedStoreItem;
            set
            {
                selectedStoreItem = value;
                OnPropertyChanged(null);
            }
        }

        public string Text
        {
            get => SelectedStoreItem?.Text;
            set
            {
                if (SelectedStoreItem != null)
                {
                    SelectedStoreItem.Text = value;
                }
                OnPropertyChanged();
            }
        }

        public string Unit
        {
            get => SelectedStoreItem?.Unit;
            set
            {
                if (SelectedStoreItem != null)
                {
                    SelectedStoreItem.Unit = value;
                }
                OnPropertyChanged();
            }
        }

        public string Barcode
        {
            get => SelectedStoreItem?.Barcode;
            set
            {
                if (SelectedStoreItem != null)
                {
                    SelectedStoreItem.Barcode = value;
                }
                OnPropertyChanged();
            }
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Text)
                && !string.IsNullOrWhiteSpace(Unit)
                && SelectedStoreItem != null;
        }

        private void OnCancel()
        {
            ClearSelection();
        }

        private async void OnSave()
        {
            await ShoppingListDataStore.UpdateStoreItemAsync(SelectedStoreItem);
            ClearSelection();
        }

        private void ClearSelection()
        {
            SelectedStoreItem = null;
            SearchText = "";
            SearchResult = null;
        }

        private async void OnScan()
        {
            ScanPage sp = new ScanPage();
            sp.OnScan += Sp_OnScan;
            await Application.Current.MainPage.Navigation.PushAsync(sp);
        }

        private void Sp_OnScan(string obj)
        {
            Barcode = obj;
        }

        private bool ValidateScan()
        {
            return SelectedStoreItem != null;
        }
    }
}

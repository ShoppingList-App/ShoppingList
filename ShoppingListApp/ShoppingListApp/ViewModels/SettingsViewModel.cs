using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    class SettingsViewModel : BaseShoppingListViewModel
    {
        public Command RecalculateItemSortCommand { get; }
        public Command DeleteDatabaseCommand { get; }

        public SettingsViewModel()
        {
            Title = "Settings";
            RecalculateItemSortCommand = new Command(RecalculateItemSort);
            DeleteDatabaseCommand = new Command(DeleteDatabase);
        }

        private async void RecalculateItemSort()
        {
            await ShoppingListDataStore.RecalculateStoreItemSort();
        }

        private async void DeleteDatabase()
        {
            await ShoppingListDataStore.ResetDatabaseAsync();
        }
    }
}

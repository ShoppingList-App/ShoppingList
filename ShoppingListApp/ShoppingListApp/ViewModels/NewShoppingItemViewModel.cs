using ShoppingListApp.Models;
using ShoppingListApp.ViewModels;
using System;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    [QueryProperty("ShoppingListId", "ShoppingListId")]
    class NewShoppingItemViewModel : BaseShoppingListViewModel
    {
        private String text;
        private String amount;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewShoppingItemViewModel()
        {
            Title = "New Shopping Item";
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            // erzeugt eine neue Funktion die zu PropertyChangedEventHandler passt und ruft darun SaveCommand.ChangeCanExecute auf => WTF????
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        public string Amount
        {
            get => amount;
            set => SetProperty(ref amount, value);
        }

        public string ShoppingListId { get; set; }

        private bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Text)
                && !string.IsNullOrWhiteSpace(Amount);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"..?{nameof(ShoppingListViewModel.ShoppingListId)}={ShoppingListId}");
        }

        private async void OnSave()
        {
            ShoppingItem shoppingItem = new ShoppingItem()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Amount = Amount
            };

            _ = await DataStore.AddShoppingItemAsync(ShoppingListId, shoppingItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync($"..?{nameof(ShoppingListViewModel.ShoppingListId)}={ShoppingListId}");
        }
    }
}

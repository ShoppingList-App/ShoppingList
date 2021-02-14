using ShoppingListApp.Models;
using System;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels
{
    class NewShoppingListViewModel : BaseShoppingListViewModel
    {
        private String text;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewShoppingListViewModel()
        {
            Title = "New Shopping List";
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

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(Text);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            ShoppingList newShoppingList = new ShoppingList()
            {
                Text = Text
            };

            await ShoppingListDataStore.AddShoppingListAsync(newShoppingList);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

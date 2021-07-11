using dotMorten.Xamarin.Forms;
using ShoppingListApp.Models;
using ShoppingListApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewShoppingItemPage : ContentPage
    {
        private readonly NewShoppingItemViewModel viewModel;

        public NewShoppingItemPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new NewShoppingItemViewModel();
        }

        private void OnAmountEntryFocused(object sender, FocusEventArgs e)
        {
            if (sender != null && sender is Entry @amountEntry)
            {
                Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    amountEntry.CursorPosition = 0;
                    amountEntry.SelectionLength = amountEntry.Text != null ? amountEntry.Text.Length : 0;
                });
            }
        }

        private async void OnRebuyTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                //sender.ItemsSource = dataset;
                await viewModel.SearchStoreItemsAsync(sender.Text);
            }
        }

        private void OnRebuySuggestionChosen(AutoSuggestBox _, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            if (args != null && args.SelectedItem != null && args.SelectedItem is StoreItem @item)
            {
                viewModel.SelectedStoreItem = @item;
            }
        }
    }
}
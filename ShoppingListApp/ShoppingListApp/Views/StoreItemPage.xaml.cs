using dotMorten.Xamarin.Forms;
using ShoppingListApp.Models;
using ShoppingListApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreItemPage : ContentPage
	{
        private readonly StoreItemViewModel viewModel;

        public StoreItemPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StoreItemViewModel();
        }

        private async void OnSearchTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                await viewModel.SearchStoreItemsAsync(sender.Text);
            }
        }

        private void OnSearchSuggestionChosen(AutoSuggestBox _, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            if (args != null && args.SelectedItem != null && args.SelectedItem is StoreItem @item)
            {
                viewModel.SelectedStoreItem = @item;
            }
        }
    }
}
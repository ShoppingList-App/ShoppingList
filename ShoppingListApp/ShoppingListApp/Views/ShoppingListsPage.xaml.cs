using ShoppingListApp.ViewModels;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListsPage : ContentPage
    {
        private ShoppingListsViewModel _viewModel;

        public ShoppingListsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ShoppingListsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}

using ShoppingListApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewShoppingListPage : ContentPage
    {
        public NewShoppingListPage()
        {
            InitializeComponent();
            BindingContext = new NewShoppingListViewModel();
        }
    }
}
using ShoppingListApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewShoppingItemPage : ContentPage
    {
        public NewShoppingItemPage()
        {
            InitializeComponent();
            BindingContext = new NewShoppingItemViewModel();
        }
    }
}
using ShoppingListApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ShoppingListApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
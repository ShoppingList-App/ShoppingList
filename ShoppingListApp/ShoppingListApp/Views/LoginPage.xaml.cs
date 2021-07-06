using ShoppingListApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            Appearing += LoginPage_Appearing;
        }

        private void LoginPage_Appearing(object sender, EventArgs e)
        {
            // force empty fields on every page view
            host.Text = "";
            username.Text = "";
            password.Text = "";
        }
    }
}
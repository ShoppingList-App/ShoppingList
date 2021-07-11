using Newtonsoft.Json;
using ShoppingListApp.Models;
using ShoppingListApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        private readonly LoginViewModel model = new LoginViewModel();

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = model;
            Appearing += LoginPage_Appearing;
        }

        private void LoginPage_Appearing(object sender, EventArgs e)
        {
            if (model.Barcode != null && model.Barcode != string.Empty)
            {
                // set values by barcode
                try
                {
                    Config loginConf = JsonConvert.DeserializeObject<Config>(model.Barcode);
                    host.Text = loginConf.Host;
                    username.Text = loginConf.Username;
                    password.Text = loginConf.Password;
                }
                catch { }
            } else
            {
                // force empty fields on every page view
                host.Text = "";
                username.Text = "";
                password.Text = "";
            }
        }
    }
}
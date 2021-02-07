using ShoppingListApp.Services;
using ShoppingListApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockShoppingListDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

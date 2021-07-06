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
            zxing.Options.PossibleFormats = new System.Collections.Generic.List<ZXing.BarcodeFormat>
            {
                ZXing.BarcodeFormat.QR_CODE
            };
        }

        private void LoginPage_Appearing(object sender, EventArgs e)
        {
            // force empty fields on every page view
            host.Text = "";
            username.Text = "";
            password.Text = "";
        }

        private void Scan_Clicked(object sender, EventArgs e)
        {
            form.IsVisible = false;
            scanner.IsVisible = true;
            zxing.IsScanning = true;
            zxing.IsAnalyzing = true;
        }

        private void ScanCancel_Clicked(object sender, EventArgs e)
        {
            zxing.IsScanning = false;
            zxing.IsAnalyzing = false;
            scanner.IsVisible = false;
            form.IsVisible = true;
        }
    }
}
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
        }

        private void Zxing_OnScanResult(ZXing.Result result)
        {
            _ = Shell.Current.GoToAsync("..?Barcode=" + result.Text);
        }

        private void ScanCancel_Clicked(object sender, EventArgs e)
        {
            _ = Shell.Current.GoToAsync("..");
        }
    }
}
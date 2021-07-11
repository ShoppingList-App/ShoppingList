using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        public event Action<string> OnScan;

        public ScanPage()
        {
            InitializeComponent();
        }

        private void Zxing_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                OnScan?.Invoke(result.Text);
                _ = Navigation.PopAsync();
            });
        }

        private async void ScanCancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopAsync();
        }
    }
}
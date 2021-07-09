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
            //zxing.Options.PossibleFormats = new System.Collections.Generic.List<ZXing.BarcodeFormat>
            //{
            //    ZXing.BarcodeFormat.QR_CODE
            //};
        }

        private void Zxing_OnScanResult(ZXing.Result result)
        {
            if (result.BarcodeFormat == ZXing.BarcodeFormat.QR_CODE)
            {
                Shell.Current.GoToAsync("..?Barcode=" + result.Text);
            }
        }

        private void ScanCancel_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("..");
        }
    }
}
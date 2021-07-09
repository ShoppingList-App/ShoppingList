using Newtonsoft.Json;
using ShoppingListApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShoppingListApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharePage : ContentPage
    {
        public SharePage()
        {
            InitializeComponent();
            QRCode.BarcodeValue = GetBarcodeValue();
            SizeChanged += SharePage_SizeChanged;
        }

        private void SharePage_SizeChanged(object sender, System.EventArgs e)
        {
            QRCode.BarcodeOptions.Width = (int)layout.Width;
            QRCode.BarcodeOptions.Height = (int)layout.Height;
        }

        private string GetBarcodeValue()
        {
            Config conf = Config.FromApplicationProperties(Application.Current);
            return JsonConvert.SerializeObject(conf);
        }
    }
}
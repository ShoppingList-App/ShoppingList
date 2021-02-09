using ShoppingListApp.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ShoppingListApp.ViewModels.Converter
{
    class StoreItemId2TextConverter : IValueConverter
    {
        public IShoppingListDataStore ShoppingListDataStore => DependencyService.Get<IShoppingListDataStore>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string @storeItemId ? ShoppingListDataStore.GetStoreItemAsync(storeItemId).Result.Text : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

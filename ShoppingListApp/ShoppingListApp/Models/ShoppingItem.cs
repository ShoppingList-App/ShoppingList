using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShoppingListApp.Models
{
    public class ShoppingItem
    {
        public String Id { get; set; }
        public string Text { get; set; }
        public string Amount { get; set; }

        public string DisplayText => Text + ": " + Amount;
    }
}

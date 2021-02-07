using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ShoppingListApp.Models
{
    public class ShoppingItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public uint Amount { get; set; }
        public string Unit { get; set; }

        public string AmountDisplay => $"{Amount} {Unit}";
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListApp.Models
{
    public class ShoppingList
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public List<ShoppingItem> Items { get; set; }
    }
}

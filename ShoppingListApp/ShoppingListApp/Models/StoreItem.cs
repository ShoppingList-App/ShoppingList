using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListApp.Models
{
    public class StoreItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Unit { get; set; }
        public uint SortKey { get; set; }
    }
}

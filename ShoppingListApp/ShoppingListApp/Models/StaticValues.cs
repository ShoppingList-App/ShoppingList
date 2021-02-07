using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListApp.Models
{
    public class StaticValues
    {
        public static List<string> Units => new List<string>()
        {
            "Einzel",
            "Fass",
            "Flasche",
            "Kiste",
            "Stiege"
        };
    }
}

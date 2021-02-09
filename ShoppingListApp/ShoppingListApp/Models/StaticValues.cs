using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingListApp.Models
{
    public class StaticValues
    {
        public static List<string> Units => new List<string>()
        {
            "VPE",
            "Bund",
            "Fass",
            "Flasche",
            "Kiste",
            "Stiege"
        };
    }
}

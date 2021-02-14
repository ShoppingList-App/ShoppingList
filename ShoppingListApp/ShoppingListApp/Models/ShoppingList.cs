using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace ShoppingListApp.Models
{
    public class ShoppingList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ShoppingItem> Items { get; set; } = new List<ShoppingItem>();

        public override bool Equals(object obj)
        {
            return (obj is ShoppingList @sl) && sl.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

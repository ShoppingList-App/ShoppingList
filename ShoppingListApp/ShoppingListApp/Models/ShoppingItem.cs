using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ShoppingListApp.Models
{
    public class ShoppingItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(ShoppingList))]     // Specify the foreign key
        public int ShoppingListId { get; set; }
        [ForeignKey(typeof(StoreItem))]
        public int StoreItemId { get; set; }
        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        public StoreItem StoreItem { get; set; }
        public uint Amount { get; set; }
        public string Unit { get; set; }

        public string AmountDisplay => $"{Amount} {Unit}";

        public override bool Equals(object obj)
        {
            return (obj is ShoppingItem @si) && si.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

using SQLite;

namespace ShoppingListApp.Models
{
    public class StoreItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public string Text { get; set; }
        public string Unit { get; set; }
        [Indexed]
        public uint SortKey { get; set; }

        public override string ToString()
        {
            return $"{Text} ({Unit})";
        }

        public override bool Equals(object obj)
        {
            return (obj is StoreItem @si) && si.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}

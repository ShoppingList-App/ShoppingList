using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Runtime.Serialization;
using System.Text;

namespace ShoppingListApp.Models
{
    [DataContract]
    public class ShoppingItem
    {
        [PrimaryKey, AutoIncrement]
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [ForeignKey(typeof(ShoppingList))]     // Specify the foreign key
        [DataMember(Name = "shoppingListId")]
        [JsonProperty(PropertyName = "shoppingListId")]
        public int ShoppingListId { get; set; }

        [ForeignKey(typeof(StoreItem))]
        [DataMember(Name = "storeItemId")]
        [JsonProperty(PropertyName = "storeItemId")]
        private int StoreItemId { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        [DataMember(Name = "storeItem")]
        [JsonProperty(PropertyName = "storeItem")]
        public StoreItem StoreItem { get; set; }

        [DataMember(Name = "amount")]
        [JsonProperty(PropertyName = "amount")]
        public uint Amount { get; set; }

        [DataMember(Name = "unit")]
        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        public bool IsSorted => StoreItem.SortKey.HasValue;

        public string AmountDisplay => $"{Amount} {Unit}";

        public override bool Equals(object obj)
        {
            return (obj is ShoppingItem @si) && si.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Get the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class ShoppingItem {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  ShoppingListId: ").Append(ShoppingListId).Append("\n");
            sb.Append("  StoreItemId: ").Append(StoreItemId).Append("\n");
            sb.Append("  StoreItem: ").Append(StoreItem).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Unit: ").Append(Unit).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Get the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}

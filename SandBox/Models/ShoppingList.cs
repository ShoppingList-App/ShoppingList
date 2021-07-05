using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace ShoppingListApp.Models
{
    public class ShoppingList
    {
        [PrimaryKey, AutoIncrement]
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [DataMember(Name = "text")]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]

        [DataMember(Name = "items")]
        [JsonProperty(PropertyName = "items")]
        public List<ShoppingItem> Items { get; set; } = new List<ShoppingItem>();

        public override bool Equals(object obj)
        {
            return (obj is ShoppingList @sl) && sl.Id == Id;
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
            var sb = new StringBuilder();
            sb.Append("class ShoppingList {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Items: ").Append(Items).Append("\n");
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

using Newtonsoft.Json;
using SQLite;
using System.Runtime.Serialization;

namespace ShoppingListApp.Models
{
    public class StoreItem
    {
        [PrimaryKey, AutoIncrement]
        [DataMember(Name = "id")]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Indexed]
        [DataMember(Name = "text")]
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [DataMember(Name = "unit")]
        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        [JsonProperty(PropertyName = "barcode")]
        public string Barcode { get; set; }

        [Indexed]
        [DataMember(Name = "sortKey")]
        [JsonProperty(PropertyName = "sortKey")]
        public uint? SortKey { get; set; }

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

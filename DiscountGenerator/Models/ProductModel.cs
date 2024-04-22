using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DiscountGenerator.Models
{
    public class ProductModel
    {
        [JsonPropertyName("ProdId")]
        public int ProdId { get; set; }

        [JsonPropertyName("ProductName")]
        public string? ProductName { get; set; }

        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
    }
}

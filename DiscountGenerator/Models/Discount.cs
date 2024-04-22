using System.Text.Json.Serialization;

namespace DiscountGenerator.Models
{
    public class DiscountModel
    {
        [JsonPropertyName("Percent")]
        public decimal? Percent { get; set; }

        [JsonPropertyName("Fix")]
        public decimal? Fix { get; set; }

        [JsonPropertyName("ProductId")]
        public int ProductId { get; set; }
    }
}

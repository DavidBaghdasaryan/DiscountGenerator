namespace DiscountGenerator.Models
{
    public class DiscountModel
    {
        public decimal? Percent { get; set; }
        public decimal? Fix { get; set; }
        public int ProductId { get; set; }
    }
}

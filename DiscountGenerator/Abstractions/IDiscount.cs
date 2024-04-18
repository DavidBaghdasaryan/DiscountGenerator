namespace DiscountGenerator.Abstractions
{
    public interface IDiscount
    {
        Task GetInfo();
        Task PostInfo();
        Task PostDiscount();
    }
}

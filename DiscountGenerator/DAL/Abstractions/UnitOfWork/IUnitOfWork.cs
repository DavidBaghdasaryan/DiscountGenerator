namespace DiscountGenerator.DAL.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductInfoRepository ProductInfoRepository { get; }
    }
}

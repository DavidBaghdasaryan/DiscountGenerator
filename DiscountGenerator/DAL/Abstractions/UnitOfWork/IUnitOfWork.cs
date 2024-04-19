namespace DiscountGenerator.DAL.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductInfoRepository ProductInfoRepository { get; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
        string GetValuesFromApps(string section);
    }
}

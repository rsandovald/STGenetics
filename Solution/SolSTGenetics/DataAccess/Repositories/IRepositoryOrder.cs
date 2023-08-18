using Models.DataEntities;

namespace DataAccess.Repositories
{
    public interface IRepositoryOrder
    {
        Task AddOrder(Order data);
        Task AddOrderDetail(ICollection<OrderDetail> detail);
    }
}
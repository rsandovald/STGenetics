using Models.DataEntities;
using Models.DTOs;

namespace BusinessLogic
{
    public interface IOrderBL
    {
        Task<OrderAddResponse> Add(OrderAddRequest data);
        decimal calculateOrderDetailDiscount(OrderDetail detail);
        decimal calculateOrderDiscount(Order order);
        decimal calculateOrderFreight(Order order);
        Task<decimal> getDetailUnitPrice(Guid animalId);
    }
}
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RepositoryOrder : IRepositoryOrder
    {
        private readonly string connectionString;

        public RepositoryOrder(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddOrder(Order data)
        {
            using var connection = new SqlConnection(connectionString);
            _ = await connection.ExecuteAsync(@"INSERT INTO [dbo].[Orders]
                                                   ([OrderId]
                                                   ,[Date]
                                                   ,[CustomerId]
                                                   ,[SubTotal]
                                                   ,[DiscountPercentage]
                                                   ,[Freight])
                                             VALUES
                                                   (@OrderId
                                                   ,@Date
                                                   ,@CustomerId
                                                   ,@SubTotal
                                                   ,@DiscountPercentage
                                                   ,@Freight)", data);

            await this.AddOrderDetail(data.OrderDetails);
        }

        public async Task AddOrderDetail(ICollection<OrderDetail> detail)
        {
            using var connection = new SqlConnection(connectionString);
            _ = await connection.ExecuteAsync(@"INSERT INTO [dbo].[OrderDetails]
                                               ([OrderDetailId]
                                               ,[OrderId]
                                               ,[AnimalId]
                                               ,[Quantity]
                                               ,[unitPrice]
                                               ,[DiscountPercentage])
                                         VALUES
                                               (@OrderDetailId
                                               ,@OrderId
                                               ,@AnimalId
                                               ,@Quantity
                                               ,@unitPrice
                                               ,@DiscountPercentage) ", detail);
        }



    }
}

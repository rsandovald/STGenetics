using AutoMapper;
using Azure.Core;
using DataAccess.Repositories;
using Models.DataEntities;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BusinessLogic
{
    public class OrderBL : IOrderBL
    {
        IRepositoryOrder _repositoryOrder;
        IMapper _mapper;
        IRepositoryAnimal _repositoryAnimal;

        public OrderBL(IRepositoryOrder repositoryOrder, IMapper mapper, IRepositoryAnimal repositoryAnimal)
        {
            this._repositoryOrder = repositoryOrder;
            _mapper = mapper;
            this._repositoryAnimal = repositoryAnimal;
        }

        public async Task<OrderAddResponse> Add(OrderAddRequest data)
        {
            TransactionResultDto transactionResultDto = null; 

            try
            {
                if (!this.validateInputData (data, transactionResultDto))
                {
                    return new OrderAddResponse()
                    {
                        TransactionResult = transactionResultDto, 
                        OrderId = Guid.Empty,
                        Total = 0,
                        OrderInserted = null
                    };
                }

                Order order = await this.loadOrder(data);
                await _repositoryOrder.AddOrder(order);

                return new OrderAddResponse()
                {
                    TransactionResult = new TransactionResultDto()
                    {
                        Code = 200,
                        Description = "Ok"
                    },
                    OrderId = order.OrderId,
                    Total = order.Total,
                    OrderInserted = order
                };


            }
            catch (ArgumentException ex)
            {
                return new OrderAddResponse()
                {
                    TransactionResult = new TransactionResultDto()
                    {
                        Code = 404,
                        Description = "Not Found. " + ex.Message
                    },
                    OrderId = Guid.Empty,
                    Total = 0,
                    OrderInserted = null
                };
            }
            catch (Exception ex)
            {
                return new OrderAddResponse()
                {
                    TransactionResult = new TransactionResultDto()
                    {
                        Code = 500,
                        Description = "Internal Error. " + ex.Message
                    },
                    OrderId = Guid.Empty,
                    Total = 0,
                    OrderInserted = null
                };
            }
        }

        public bool validateInputData(OrderAddRequest data, TransactionResultDto transactionResultDto)
        {
            transactionResultDto = null; 
            bool result = true;

            if (data.OrderDetails == null)
                result=  false;
            

            if (data.OrderDetails.Count () == 0)
                result  = false;

            if (!result)
            {
                transactionResultDto = new TransactionResultDto()
                {
                    Code = (int)TransactionResultCodes.BadRequest,
                    Description = "BadRequest" + ". There are not order details items"
                };

                return result; 
            }
            var uniques = data.OrderDetails.DistinctBy(d => d.AnimalId).Count();

            if (uniques != data.OrderDetails.Length)
            {
                result = false;
                transactionResultDto = new TransactionResultDto()
                {
                    Code = (int)TransactionResultCodes.BadRequest,
                    Description = "BadRequest" + ". There are duplicated order details items"
                };

                return result;
            }
        
            return true; 

        }
        

        public async Task<Order> loadOrder(OrderAddRequest data)
        {
            Order order = new Order();
            order.OrderId = Guid.NewGuid();
            order.Date = DateTime.Now;
            order.CustomerId = data.CustomerId;

            foreach (var detail in data.OrderDetails)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderDetailId = Guid.NewGuid();
                orderDetail.OrderId = order.OrderId;
                orderDetail.Quantity = detail.Quantity;
                orderDetail.AnimalId = detail.AnimalId;
                orderDetail.DiscountPercentage = this.calculateOrderDetailDiscount(orderDetail);
                orderDetail.unitPrice = await this.getDetailUnitPrice(orderDetail.AnimalId);

                order.OrderDetails.Add(orderDetail);
            }

            order.DiscountPercentage = this.calculateOrderDiscount(order);
            order.Freight = this.calculateOrderFreight(order);

            return order;

        }

        public decimal calculateOrderDetailDiscount(OrderDetail detail)
        {
            decimal result = 0;

            if (detail.Quantity > 50)
                result = 5;

            return result;
        }

        public decimal calculateOrderDiscount(Order order)
        {
            int totalOfAnimals;
            decimal result = 0;

            totalOfAnimals = order.OrderDetails.Sum(o => o.Quantity);

            if (totalOfAnimals > 200)
                result = 3;

            return result;
        }

        public decimal calculateOrderFreight(Order order)
        {
            int totalOfAnimals;
            decimal result = 1000;

            totalOfAnimals = order.OrderDetails.Sum(o => o.Quantity);

            if (totalOfAnimals > 300)
                result = 0;
            return result;
        }

        public async Task<decimal> getDetailUnitPrice(Guid animalId)
        {
            decimal result = 0;
            var animal = await this._repositoryAnimal.Get(animalId);
           
            if (animal != null)
                result = animal.Price;
            else
                throw new ArgumentException($"The animalId {animalId} does not exist.");

            return result;
        }



    }
}

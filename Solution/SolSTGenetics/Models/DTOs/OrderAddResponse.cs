using Models.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public  class OrderAddResponse
    {
        public TransactionResultDto TransactionResult { get; set; }
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public Order OrderInserted { get; set; }
    }
}

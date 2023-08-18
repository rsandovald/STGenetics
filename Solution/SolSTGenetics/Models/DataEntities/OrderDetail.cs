using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataEntities
{
    public partial class OrderDetail
    {
        public Guid OrderDetailId { get; set; }

        public Guid OrderId { get; set; }

        public Guid AnimalId { get; set; }

        public int Quantity { get; set; }

        public decimal DiscountPercentage { get; set; }

        public decimal unitPrice { get; set; }

        public decimal Total
        {
            get
            {
                decimal result;

                result = unitPrice * Quantity;
                decimal totalDiscount = 0; 
                     
                if (DiscountPercentage != 0)
                {
                    totalDiscount = ((unitPrice * DiscountPercentage) / 100) * Quantity;
                    result = result - totalDiscount; 
                }

                return result; 
            }
        }
        public virtual Animal Animal { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;


    }

}

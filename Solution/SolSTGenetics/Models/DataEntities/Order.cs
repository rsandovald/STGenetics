using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataEntities
{
    public partial class Order
    {
        public Guid OrderId { get; set; }

        public DateTime Date { get; set; }

        public string CustomerId { get; set; }

        public decimal SubTotal
        {
            get
            {
                decimal result = 0 ; 
                if (this.OrderDetails != null && this.OrderDetails.Count > 0 )
                {
                    result = this.OrderDetails.Sum (detail => detail.Total) ;
                }
                return result; 
            }
        }

        public decimal Total
        {
            get
            {
                decimal result;
                decimal subtotal = SubTotal; 

                if (DiscountPercentage == 0)
                    result = subtotal  ;
                else
                    result = subtotal - ((subtotal * DiscountPercentage) / 100);

                return result + Freight;

            }
        }

        public decimal DiscountPercentage { get; set; }

        public decimal Freight { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

}

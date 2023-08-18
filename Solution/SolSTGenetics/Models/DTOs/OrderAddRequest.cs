using Models.DataEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public  class OrderAddRequest
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string CustomerId { get; set; }
        [Required]
        public virtual OrderDetailAddRequest[] OrderDetails { get; set; }
    }
}

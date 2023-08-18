using Models.DataEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public  class OrderDetailAddRequest
    {
        [Required]
        public Guid AnimalId { get; set; }

        [Required]  
        public int Quantity { get; set; }

    }
}

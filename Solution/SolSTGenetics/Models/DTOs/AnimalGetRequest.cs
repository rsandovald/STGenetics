using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public  class AnimalGetRequest
    {
        public Guid AnimalId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;

        [Range(-1, 1, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public short Status { get; set; } = -1 ; 
    }
}

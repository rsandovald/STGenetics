using Models.AttributeValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public  class AnimalBaseDto
    {

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(maximumLength: 50)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(maximumLength: 50)]
        public string Breed { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(maximumLength: 1)]

        [SexAttribute]
        public string Sex { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [Range(0, 1, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public short Status { get; set; }
    }
}

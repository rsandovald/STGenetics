using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataEntities
{
    public class Animal
    {
        public Guid AnimalId { get; set; }

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
        //TODO: [StringRange("M", "F")]
        public string Sex { get; set; } = null!;

        [Required(ErrorMessage = "The field {0} is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public short Status { get; set; }

    }

}

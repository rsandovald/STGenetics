using Models.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    [Serializable]
    public  class AnimalDeleteResponse
    {
        public TransactionResultDto TransactionResult { get; set; }
        public Animal AnimalDeleted { get; set; }
    }
}

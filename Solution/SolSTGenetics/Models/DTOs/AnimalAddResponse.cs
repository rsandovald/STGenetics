﻿using Models.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public  class AnimalAddResponse
    {
        public TransactionResultDto TransactionResult { get; set; }
        public Animal AnimalInserted { get; set; }
    }
}

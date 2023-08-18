using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public enum TransactionResultCodes
    {
        Ok = 200,
        Error = 500,
        NotFound = 404, 
        BadRequest = 400

    }
    public  class TransactionResultDto
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }
}

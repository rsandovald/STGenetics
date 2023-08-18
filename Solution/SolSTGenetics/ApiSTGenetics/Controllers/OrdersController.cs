using BusinessLogic;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataEntities;
using Models.DTOs;

namespace ApiSTGenetics.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        IOrderBL _orderBL;

        public OrdersController(IOrderBL orderBL)
        {
            this._orderBL = orderBL;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] OrderAddRequest orderAddRequest)
        {
            var result = await this._orderBL.Add (orderAddRequest);
            return StatusCode(result.TransactionResult.Code, result);
        }
    }
}

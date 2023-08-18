using BusinessLogic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace ApiSTGenetics.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/accounts")]
    [ApiController]
    public class AccountController : Controller
    {
        IAccountBL _accountBL; 
        public AccountController(IAccountBL accountBL)
        {
            this._accountBL = accountBL;            
        }

        [HttpPost("login")]
        public async Task<ActionResult<AccountLoginResponse>> Login([FromBody] AccountLoginRequest credentials)
        {
            var result = this._accountBL.Login(credentials); 
            return StatusCode(result.TransactionResult.Code, result);
        }
    }
}

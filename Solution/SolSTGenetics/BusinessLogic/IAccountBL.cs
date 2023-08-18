using Models.DTOs;

namespace BusinessLogic
{
    public interface IAccountBL
    {
        AccountLoginResponse Login(AccountLoginRequest credentials);
    }
}
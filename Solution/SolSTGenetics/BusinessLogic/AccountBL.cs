using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class AccountBL : IAccountBL
    {
        IConfiguration _configuration;
        public AccountBL(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public AccountLoginResponse Login(AccountLoginRequest credentials)
        {

            if (credentials.Email == this._configuration["UserEmail"]
                && credentials.Password == this._configuration["UserPassword"])

                return buildToken(credentials);

            else
                return new AccountLoginResponse()
                {
                    TransactionResult = new TransactionResultDto()
                    {
                        Code = 400,
                        Description = "Bad Request" + ". Invalid User"
                    }
                };
        }

        private AccountLoginResponse buildToken(AccountLoginRequest credentials)
        {
            var claims = new List<Claim>()
            {
                new Claim("email", credentials.Email)
            };

            /*
            var usuario = await userManager.FindByEmailAsync(credenciales.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);
            */

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["KeyJwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AccountLoginResponse()
            {
                TransactionResult = new TransactionResultDto()
                {
                    Code = 200,
                    Description = "Ok"
                },
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };

        }

    }
}

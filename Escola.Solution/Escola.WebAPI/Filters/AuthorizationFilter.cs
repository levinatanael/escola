using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace Escola.WebAPI.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token) || !ValidateToken(token))
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private static bool ValidateToken(string token)
        {
            try
            {
                return new JwtSecurityTokenHandler().ReadToken(token) is JwtSecurityToken jwtToken && jwtToken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }
    }
}
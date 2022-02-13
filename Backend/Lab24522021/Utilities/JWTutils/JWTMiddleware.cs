using Licenta.Services.AuthService;
using Licenta.Utilities.JWTutils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Utilities
{
    public class JWTMiddleware
    {

        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContext httpContext, IUserService userService, IJWTUtils jWTUtils)
        {

            // httpcontex avem un header si un body cu informatiile propiu zise. Authorization se gasesete in header
            //asta trebuie facut in frontend 
            //Berar -token- facem split ca sa luam partea de final
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
            var userId = jWTUtils.ValidateJWTToken(token);
            if(userId!=Guid.Empty)
            {
                httpContext.Items["User"] = userService.GetById(userId);
            }
            await _next(httpContext);
        }
    }
}

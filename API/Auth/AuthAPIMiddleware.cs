using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StudentServiceAPI.Models;
using StudentServiceBL.Logging;
using System.Threading.Tasks;

namespace StudentServiceAPI.Auth
{
    public class AuthAPIMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthAPIMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthTokenChecker authChecker, ILogFactory logFactory)
        {
            var token = context.Request.Headers["token"].ToString();
            if (authChecker.IsValidToken(token))
            {
                await _next.Invoke(context);
                return;
            }
            else
            {
                var res = new ApiResponseBase();
                res.SetUnauthorizedResponse(context.Response, "Invalid token");                
                await context.Response.WriteAsync(JsonConvert.SerializeObject(res, new JsonSerializerSettings { Formatting = Formatting.Indented }));
                return;
            }
        }
    }
}

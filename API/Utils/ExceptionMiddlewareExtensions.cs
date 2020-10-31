using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using StudentServiceAPI.Models;
using StudentServiceBL.Logging;
using System.Reflection;

namespace StudentServiceAPI.Utils
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogFactory logFactory)
        {
            var logger = logFactory.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Something went wrong: {contextFeature.Error}");
                        var res = new ApiResponseBase();
                        res.SetInternalErrorResponse(context.Response, "Internal Server Error." + contextFeature.Error.Message);
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(res));
                    }
                });
            });
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member



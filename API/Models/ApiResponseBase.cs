using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Net;

namespace StudentServiceAPI.Models
{
    /// <summary>
    /// Base class for all response
    /// </summary>
    public class ApiResponseBase
    {
        /// <summary>
        /// Responce code
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ApiResponseCodeEnum Code { get; set; }
        /// <summary>
        /// Response message
        /// </summary>
        public string Message { get; set; }

        internal void SetNotFoundResponse(HttpResponse response,string message="")
        {
            response.StatusCode = (int)HttpStatusCode.NotFound;
            Code = ApiResponseCodeEnum.NotFound;
            Message = message;
        }

        internal void SetSuccessResponse(HttpResponse response, string message = "")
        {
            response.StatusCode = (int)HttpStatusCode.OK;
            Code = ApiResponseCodeEnum.Success;
            Message = message;
        }

        internal void SetUnauthorizedResponse(HttpResponse response, string message)
        {
            response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Code = ApiResponseCodeEnum.Unauthorized;
            Message = message;
        }

        internal void SetInternalErrorResponse(HttpResponse response, string message ="")
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            Code = ApiResponseCodeEnum.InternalError;
            Message = message;

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace StudentServiceAPI.Controllers
{
    /// <summary>
    /// Maintanance functions
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        /// <summary>
        /// Check API working
        /// </summary>
        /// <returns></returns>
        [HttpGet("check")]
        public string Check()            
        {
            var version = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            return "Hello,this is student service API. Version " + version;
        }

        /// <summary>
        /// Check of correct processed unhandled exception
        /// </summary>
        /// <returns></returns>
        [HttpGet("throw")]
        public string ThrowException()
        {
            throw new InvalidOperationException("This is test exception. Nothing happens - only test");
        }
    }
}

using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using StudentServiceAPI.Auth;
using StudentServiceAPI.Models;
using StudentServiceAPI.Utils;
using StudentServiceBL;
using StudentServiceBL.Logging;
using StudentServiceBL.Storage;
using StudentServiceStorage;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace StudentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));
            var repo = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            XmlConfigurator.Configure(repo, log4netConfig["configuration"]["log4net"]);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });

            services.AddScoped<IStudentServiceWorker, StudentServiceWorker>();
            services.AddScoped<IStudentServiceStorage>(s =>
            {
                var connectionString = Configuration.GetConnectionString("Default");
                return new PostgresStorage(connectionString);
            });
            services.AddScoped<IAuthTokenChecker>(provider =>
            {
                var tokenCheckOption = new AuthTokenCheckerOptions();
                Configuration.GetSection("TokenCheck").Bind(tokenCheckOption);
                var loggerFactory = provider.GetRequiredService<ILogFactory>();
                //return new AuthTokenChecker(tokenCheckOption, loggerFactory); //correct token //eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicXdlcnR5IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoidXNlciIsIm5iZiI6MTQ4MTYzOTMxMSwiZXhwIjoxNDgxNjM5MzcxLCJpc3MiOiJNeUF1dGhTZXJ2ZXIiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjUxODg0LyJ9.dQJF6pALUZW3wGBANy_tCwk5_NR0TVBwgnxRbblp5Ho
                return new AuthTokenCheckerAlwaysTrue();
            });
            services.AddTransient<ILogFactory, LogSystem>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Student service API", Version = "v1" });
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureExceptionHandler(new LogSystem());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.EnableFilter();
            });
                        
            app.UseRouting();

            app.Use(async (context, next) =>
            {
                Endpoint endpoint = context.GetEndpoint();
                if (endpoint != null)                {
                    
                    var routePattern = (endpoint as Microsoft.AspNetCore.Routing.RouteEndpoint)?.RoutePattern?.RawText;
                    await next();
                }
                else
                {

                    var res = new ApiResponseBase();
                    res.SetNotFoundResponse(context.Response,"Function not found");
                    context.Response.ContentType = "application/json; charset=utf-8";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(res));
                }
            });

            app.UseMiddleware<AuthAPIMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication()
                .AddGoogle(googleOptions => 
                {
                    googleOptions.ClientId = config.GetValue<string>("GmailApi:client_id");
                    googleOptions.ClientSecret = config.GetValue<string>("GmailApi:client_secret");
                });
            return services;
        }
    }
}
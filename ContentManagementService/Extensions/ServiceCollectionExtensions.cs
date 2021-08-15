using ContentManagementService.Handlers;
using ContentManagementService.Interfaces;
using ContentManagementService.Services;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace ContentManagementService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddContentManagementService(this IServiceCollection services, string baseUrl)
        {
            services.AddTransient<AuthTokenHeaderHandler>();

            services.AddRefitClient<IContentManagementClient>(settings: new RefitSettings { ContentSerializer = new NewtonsoftJsonContentSerializer() })
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl))
                .AddHttpMessageHandler<AuthTokenHeaderHandler>();

            services.AddScoped<IContentManagementApiService, ContentManagementApiService>();
        }
    }
}
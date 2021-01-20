using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Raccoon.Devkits.DynamicProxy
{
    public static class DynamicProxyExtensions
    {
        public static IServiceCollection AddTransientProxy<TIService, TImplementation, TProxy>(this IServiceCollection services) 
            where TIService : class 
            where TImplementation : class, TIService 
            where TProxy : DynamicProxyBase<TIService> =>
            services.AddTransient<TImplementation>()
                .AddTransient(RegisterServiceProxy<TIService, TImplementation, TProxy>);
        public static IServiceCollection AddScopedProxy<TIService, TImplementation, TProxy>(this IServiceCollection services)
           where TIService : class
           where TImplementation : class, TIService
           where TProxy : DynamicProxyBase<TIService> =>
           services.AddScoped<TImplementation>()
               .AddScoped(RegisterServiceProxy<TIService, TImplementation, TProxy>);

        public static IServiceCollection AddSingletonProxy<TIService, TImplementation, TProxy>(this IServiceCollection services)
            where TIService : class
            where TImplementation : class, TIService
            where TProxy : DynamicProxyBase<TIService> =>
            services.AddSingleton<TImplementation>()
                .AddSingleton(RegisterServiceProxy<TIService, TImplementation, TProxy>);

        private static TIService RegisterServiceProxy<TIService, TImplementation, TProxy>(IServiceProvider serviceProvider)
            where TIService : class 
            where TImplementation : class, TIService 
            where TProxy : DynamicProxyBase<TIService>
        {
            TProxy proxy = (DispatchProxy.Create<TIService, TProxy>() as TProxy)!;
            TImplementation implementation = serviceProvider.GetRequiredService<TImplementation>();
            proxy.Target = implementation;
            proxy.ImplementationType = typeof(TImplementation);
            proxy.ServiceProvider = serviceProvider;
            return (proxy as TIService)!;
        }
    }
}

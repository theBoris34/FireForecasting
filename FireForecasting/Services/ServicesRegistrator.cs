using FireForecasting.Services.Intarface;
using Microsoft.Extensions.DependencyInjection;

namespace FireForecasting.Services
{
    static class ServicesRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IFireService, FireService>()
            .AddTransient<IUserDialog, UserDialogService>()            
            ;
    }
}

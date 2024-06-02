using Application.Gateway;
using Application.UseCaseImpl;
using Infrastructure.Mapper;
using Infrastructure.Services;
using UseCase.Interfaces;

namespace Infrastructure.Config
{
    public static class ScopedConfiguration
    {
        public static IServiceCollection ScopedConfig(this IServiceCollection services)
        {
            services.AddScoped<ProdutsMapper>();
            services.AddScoped<OrderMapper>();

            services.AddScoped<ICreateProdutUseCase, CreateProdutUseCaseImpl>();
            services.AddScoped<ICreateOrderUseCase, CreateOrderUseCaseImpl>();

            services.AddScoped<ICreateProdutGateway, CreateProdutGatewayImpl>();
            services.AddScoped<ICreateOrderGateway, CreateOrderGatewayImpl>();

            return services;
        }
    }
}

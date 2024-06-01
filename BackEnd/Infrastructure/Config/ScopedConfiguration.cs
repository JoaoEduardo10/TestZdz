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

            services.AddScoped<ICreateProdutUseCase, CreateProdutUseCaseImpl>();

            services.AddScoped<ICreateProdutGateway, CreateProdutGatewayImpl>();

            return services;
        }
    }
}

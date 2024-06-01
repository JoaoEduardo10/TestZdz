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
            services.AddScoped<RequestsMapper>();

            services.AddScoped<ICreateProdutUseCase, CreateProdutUseCaseImpl>();
            services.AddScoped<ICreateRequestUseCase, CreateRequestUseCaseImpl>();

            services.AddScoped<ICreateProdutGateway, CreateProdutGatewayImpl>();
            services.AddScoped<ICreateRequestGateway, CreateRequestGatewayImpl>();

            return services;
        }
    }
}

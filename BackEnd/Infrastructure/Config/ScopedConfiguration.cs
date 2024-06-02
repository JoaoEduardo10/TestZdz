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
            services.AddScoped<ProductMapper>();
            services.AddScoped<OrderMapper>();

            services.AddScoped<ICreateProductUseCase, CreateProductUseCaseImpl>();
            services.AddScoped<ICreateOrderUseCase, CreateOrderUseCaseImpl>();
            services.AddScoped<IGetAllProductUseCase, GetAllProductsUseCseImpl>();
            services.AddScoped<IGetAllOrdersUseCase, GetAllOrdersUseCaseImpl>();

            services.AddScoped<ICreateProductGateway, CreateProductGatewayImpl>();
            services.AddScoped<ICreateOrderGateway, CreateOrderGatewayImpl>();
            services.AddScoped<IGetAllProductsGateway, GetAllProductsGatewayImpl>();
            services.AddScoped<IGetAllOrdersGateway, GetAllOrdersGatewayImpl>();

            return services;
        }
    }
}

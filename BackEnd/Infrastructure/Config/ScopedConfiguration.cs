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
            services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCaseImpl>();
            services.AddScoped<IUpdateOrderByIdUseCase, UpdateOrderByIdUseCaseImpl>();
            services.AddScoped<IUpdateProductByIdUseCase, UpdateProductByIdUseCaseImpl>();
            services.AddScoped<IDeleteOrderByIdUseCase, DeleteOrderByIdUseCaseImpl>();
            services.AddScoped<IDeleteProductByIdUseCase, DeleteProductByIdUseCaseImpl>();
            services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCaseImpl>();

            services.AddScoped<ICreateProductGateway, CreateProductGatewayImpl>();
            services.AddScoped<ICreateOrderGateway, CreateOrderGatewayImpl>();
            services.AddScoped<IGetAllProductsGateway, GetAllProductsGatewayImpl>();
            services.AddScoped<IGetAllOrdersGateway, GetAllOrdersGatewayImpl>();
            services.AddScoped<IGetOrderByIdGateway, GetOrderByIdGatewayImpl>();
            services.AddScoped<IUpdateOrderByIdGateway, UpdateOrderByIdGatewayImpl>();
            services.AddScoped<IUpdateProductByIdGateway, UpdateProductByIdGatewayImpl>();
            services.AddScoped<IDeleteOrderByIdGateway, DeleteOrderByIdGatewayImpl>();
            services.AddScoped<IDeleteProductByIdGateway, DeleteProductByIdGatewayImpl>();
            services.AddScoped<IGetProductByIdGateway, GetProductByIdGatewayImpl>();

            return services;
        }
    }
}

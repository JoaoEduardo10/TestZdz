using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Entities;

namespace Infrastructure.Mapper
{
    public class ProductMapper
    {
        public List<Product> ToProductEntityListFromProductList(List<ProductEntity> productsEntity)
        {
            List<Product> products = [];

            foreach(ProductEntity product in  productsEntity)
            {
                products.Add(
                ToProductEntityFromProduct(product)
                );
            }

            return products;
        }

        

        public Product ToProductRequestDtoFromProduct(ProductRequestDto productEntity)
        {
            return new Product(
             productEntity.Name,
             productEntity.Value
             );
        }

        public Product ToProductEntityFromProduct(ProductEntity ProductEntity)
        {
            return new Product(
             ProductEntity.Id,
             ProductEntity.Name,
             ProductEntity.Value
             );
        }

        public Product AddIdFromProduct(int id)
        {
            return new Product(
             id
             );
        }

        public ProductEntity ToProductFromProductEntity(Product product)
        {
            return new ProductEntity(
             product.Name,
             product.Value
             );
        }

    }
}

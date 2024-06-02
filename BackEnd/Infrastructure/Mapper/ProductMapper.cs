using Core.Domain;
using Infrastructure.Dtos.Request;
using Infrastructure.Entities;

namespace Infrastructure.Mapper
{
    public class ProductMapper
    {
        public List<Product> ToProduct(List<ProductEntity> productsEntity)
        {
            List<Product> products = [];

            foreach(ProductEntity product in  productsEntity)
            {
                products.Add(
                ToProduct(product)
                );
            }

            return products;
        }

        public Product ToProduct(ProductEntity ProductEntity)
        {
            return new Product(
             ProductEntity.Id,
             ProductEntity.Name,
             ProductEntity.Value
             );
        }

        public Product ToProdut(ProductRequestDto productEntity)
        {
            return new Product(
             productEntity.Name,
             productEntity.Value
             );
        }

        public Product ToProdut(int id)
        {
            return new Product(
             id
             );
        }

        public ProductEntity ToProdutEntity(Product product)
        {
            return new ProductEntity(
             product.Name,
             product.Value
             );
        }

    }
}

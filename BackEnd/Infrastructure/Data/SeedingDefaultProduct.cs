using Core.Domain;
using Infrastructure.Mapper;

namespace Infrastructure.Data
{
    public class SeedingDefaultProduct
    {
        private readonly InfrastrutureDataBaseContext _Context;

        private readonly ProductMapper _ProductMapper;

        public SeedingDefaultProduct(InfrastrutureDataBaseContext context, ProductMapper mapper)
        {
            _Context = context;
            _ProductMapper = mapper;
        }

        public void Seed()
        {
            if(_Context.ProductsEntity.Any())
            {
                return;
            }

            var arroz = new Product("Arroz", 10.90f);
            var feijao = new Product("Feijão", 8.50f);
            var macarrao = new Product("Macarrão", 5.25f);
            var leite = new Product("Leite", 3.75f);
            var carne = new Product("Carne", 20.00f);
            var godOfWar = new Product("Jogo do God Of War", 300);

            _Context.ProductsEntity.AddRange(
                _ProductMapper.ToProductFromProductEntity(arroz),
               _ProductMapper.ToProductFromProductEntity(feijao),
               _ProductMapper.ToProductFromProductEntity(macarrao),
               _ProductMapper.ToProductFromProductEntity(leite),
               _ProductMapper.ToProductFromProductEntity(carne),
               _ProductMapper.ToProductFromProductEntity(godOfWar)
             );

            _Context.SaveChanges();
        }
    }
}

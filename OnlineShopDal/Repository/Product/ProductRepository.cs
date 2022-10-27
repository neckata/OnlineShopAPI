using OnlineShopDal.Databases.Postgres;
using System;

namespace OnlineShopDal.Repositories.Product
{
    public class ProductRepository : PostgreSqlDbRepository<Entities.Product, Guid>, IProductRepository
    {
        public ProductRepository(PostgresDbContext context) : base(context)
        {

        }
    }
}

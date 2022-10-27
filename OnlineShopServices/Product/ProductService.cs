using OnlineShopDal;
using OnlineShopDal.Repositories.Product;
using System;

namespace OnlineShopServices.Product
{
    public class ProductService : Service.CrudService<IProductRepository, OnlineShopDal.Entities.Product, Guid>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork uow) : base(uow)
        {
            _productRepository = uow.Repository<IProductRepository>();
        }
    }
}

using OnlineShopServices.Service;
using System;

namespace OnlineShopServices.Product
{
    public interface IProductService : ICrudService<OnlineShopDal.Entities.Product, Guid>
    {
    }
}

using OnlineShopServices.Service.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopServices.Service
{
    public interface ICrudService<IEntity, TKey>
    {
        /// <summary>
        /// returns entity object by given id
        /// </summary>
        /// <param name="id"></param>
        Task<DataResponse<IEntity>> GetByIdAsync(object id);

        /// <summary>
        /// returns all entity objects
        /// </summary>
        Task<DataResponse<IReadOnlyCollection<IEntity>>> GetAllAsync();

        /// <summary>
        /// creates new entity from given entity
        /// </summary>
        /// <param name="entity"></param>
        Task<Response.Response> AddAsync(IEntity entity);

        /// <summary>
        /// creates new entities as bulk insert from given entity list
        /// </summary>
        /// <param name="entityList"></param>
        Task<Response.Response> AddRangeAsync(IEntity[] entityList);

        /// <summary>
        /// updates given entity and returns affected row count.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>affected row count in db</returns>
        Task<DataResponse<int>> UpdateAsync(IEntity entity);

        /// <summary>
        /// hard or soft deletes entity by given id
        /// </summary>
        /// <param name="id"></param>
        Task<Response.Response> DeleteAsync(object id);
    }
}
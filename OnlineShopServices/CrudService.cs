using OnlineShopDal;
using OnlineShopDal.Entities;
using OnlineShopDal.Repositories;
using OnlineShopServices.Service.Response;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OnlineShopServices.Service
{
    /// <summary>
    /// Abstract class for basic create, update, delete and get operations.
    /// </summary>
    /// <typeparam name="TEntity">TEntity is db entity.</typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TKey">TKey is PK type for entity</typeparam>
    public abstract class CrudService<TRepository, TEntity, TKey> : ICrudService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TRepository : IRepository<TEntity>
    {
        protected readonly IUnitOfWork Uow;
        protected readonly TRepository Repository;

        protected CrudService(IUnitOfWork uow)
        {
            Uow = uow;
            Repository = Uow.Repository<TRepository, TEntity>();
        }

        public virtual async Task<DataResponse<TEntity>> GetByIdAsync(object id)
        {
            var businessResp = new DataResponse<TEntity>
            {
                Type = ResponseType.RecordNotFound
            };

            var entity = await Repository.GetByIdAsync(id);

            if (entity == null)
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            businessResp.Type = ResponseType.Success;
            businessResp.Data = entity;

            return businessResp;
        }

        public async Task<DataResponse<IReadOnlyCollection<TEntity>>> GetAllAsync()
        {
            var businessResp = new DataResponse<IReadOnlyCollection<TEntity>>();

            var entities = await Repository.GetAllAsync();

            if (!entities.Any())
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            businessResp.Type = ResponseType.Success;
            businessResp.Data = entities;

            return businessResp;
        }

        public virtual async Task<Response.Response> AddAsync(TEntity entity)
        {
            await Repository.AddAsync(entity);

            await Uow.SaveAsync();

            return new Response.Response
            {
                Type = ResponseType.Success,
                Data = entity.Id
            };
        }

        public async Task<Response.Response> AddRangeAsync(TEntity[] entitiesList)
        {
            await Repository.AddRangeAsync(entitiesList);

            await Uow.SaveAsync();

            return new Response.Response
            {
                Type = ResponseType.Success
            };
        }

        public virtual async Task<DataResponse<int>> UpdateAsync(TEntity entity)
        {
            var businessResp = new DataResponse<int>();

            var entityModel = await Repository.GetByIdAsync(entity.Id);

            if (entityModel == null)
            {
                businessResp.ErrorCode = ErrorCode.RecordNotFound;
                return businessResp;
            }

            var type = typeof(TEntity);
            var entityProperties = type.GetProperties();

            foreach (PropertyInfo entityProperty in entityProperties)
            {
                PropertyInfo modelProperty = typeof(TEntity).GetProperty(entityProperty.Name);

                var value = entityProperty.GetValue(entity);

                modelProperty.SetValue(entityModel, value, null);

                break;
            }

            Repository.Update(entityModel);

            var affectedRows = await Uow.SaveAsync();

            businessResp.Data = affectedRows;
            businessResp.Type = ResponseType.Success;

            return businessResp;
        }

        public virtual async Task<Response.Response> DeleteAsync(object id)
        {
            var resp = new Response.Response();

            var entity = await Repository.GetByIdAsync(id);

            if (entity == null)
            {
                resp.ErrorCode = ErrorCode.RecordNotFound;
                return resp;
            }

            var type = typeof(TEntity);

            var entityProperties = type.GetProperties();

            Repository.Remove(entity);

            await Uow.SaveAsync();

            resp.Type = ResponseType.Success;

            return resp;
        }
    }
}

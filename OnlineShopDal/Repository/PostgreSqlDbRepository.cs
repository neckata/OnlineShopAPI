﻿using Microsoft.EntityFrameworkCore;
using OnlineShopDal.Databases.Postgres;
using OnlineShopDal.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopDal.Repositories
{
    public abstract class PostgreSqlDbRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : class, IEntity<TKey>
    {
        protected readonly DbSet<TEntity> Entities;
        private readonly PostgresDbContext _context;

        protected PostgreSqlDbRepository(PostgresDbContext context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }
        public async Task AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Entities.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            var attachedEntity = Entities.Local.FirstOrDefault(e => e.Id.Equals(entity.Id));

            if (attachedEntity != null)
            {
                _context.Entry(attachedEntity).State = EntityState.Detached;
            }

            Entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            //check entity state
            var dbEntityEntry = _context.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                Entities.Attach(entity);
                Entities.Remove(entity);
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// sets and returns new entities which is different from main entity
        /// </summary>
        /// <typeparam name="TNewEntity"></typeparam>
        /// <typeparam name="TNewEntityKey"></typeparam>
        /// <returns></returns>
        protected DbSet<TNewEntity> GetEntities<TNewEntity, TNewEntityKey>() where TNewEntity : class, IEntity<TNewEntityKey>
        {
            return _context.Set<TNewEntity>();
        }
    }
}
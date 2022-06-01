﻿using Configr.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Configr.Core.DataAccess
{
    public class EntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {

        public async Task<TEntity> Add(TEntity entity)
        {
            using (var context=new TContext())
            {
               var ent= context.Entry(entity);
                ent.State = EntityState.Added;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var ent = context.Entry(entity);
                ent.State = EntityState.Deleted;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter==null?
                    await context.Set<TEntity>().ToListAsync():
                    await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var ent = context.Entry(entity);
                ent.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
        }

   
    }
}

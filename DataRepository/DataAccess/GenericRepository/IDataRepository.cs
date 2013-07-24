using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataRepository.DataAccess.GenericRepository
{
    public interface IDataRepository<TEntity> : IDisposable where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate);

        TEntity FindFirstBy(Expression<Func<TEntity, bool>> predicate);
        TEntity Find(int id);

        IQueryable<TEntity> Load();

        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Delete(IQueryable<TEntity> entities);
        void Edit(TEntity entity);

        void Save(TEntity entity);
        int Count();
        void Refresh(TEntity entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IDataRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntityId;
    }
}

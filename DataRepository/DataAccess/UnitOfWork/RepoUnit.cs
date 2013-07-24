using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using DataRepository.DataAccess.GenericRepository;
using DataRepository.Models;

namespace DataRepository.DataAccess.UnitOfWork
{
    public class RepoUnit : IUnitOfWork
    {
        private MineContext _context;

        private MineContext getContext()
        {
            return _context ?? (_context = new MineContext());
        }

        private IDataRepository<FanLog> _fanLogRepo;

        public IDataRepository<FanLog> FansLogRepo {
            get { return _fanLogRepo ?? (_fanLogRepo = new DataRepository<FanLog>(getContext())); }
        }



        public void Commit() {
            try {
                _context.SaveChanges();
            } catch (DbEntityValidationException dbEx) {
                foreach (var validationErrors in dbEx.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine("Save failed", ex);
            }
        }

        public void Dispose() {
            if (_context != null)
                _context.Dispose();
            //GC.SuppressFinalize(this);
        }

        public IDataRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IEntityId
        {
            return new DataRepository<TEntity>(_context);
        }
    }
}

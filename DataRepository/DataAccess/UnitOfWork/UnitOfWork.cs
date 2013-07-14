using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using DataRepository.DataAccess.GenericRepository;

namespace DataRepository.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context) {
            _context = context;
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
            where TEntity : class 
        {
            return new DataRepository<TEntity>(_context);
        }
    }
}

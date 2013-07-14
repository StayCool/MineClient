using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace DataRepository.DataAccess.GenericRepository
{

    public class DataRepository<TEntity> : IDataRepository<TEntity> 
        where TEntity : class
    {
        private readonly DbContext _context;

        public DataRepository(DbContext context) {
            _context = context;
        }

        public virtual IQueryable<TEntity> GetAll() {
            return _context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate) {
            return _context.Set<TEntity>().Where(predicate);
        }

        public virtual TEntity FindFirstBy(Expression<Func<TEntity, bool>> predicate) {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual void Add(TEntity entity) {
            _context.Set<TEntity>().Add(entity);
        }

        public virtual void Delete(TEntity entity) {
            _context.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(IQueryable<TEntity> entities) {
            foreach (TEntity entity in entities.ToList())
                _context.Set<TEntity>().Remove(entity);
        }

        public virtual void Edit(TEntity entity) {
            _context.Entry(entity).State = System.Data.EntityState.Modified;
        }

        public virtual int Save() {
            try {
                return _context.SaveChanges();
            } catch (DbEntityValidationException dbEx) {
                foreach (var validationErrors in dbEx.EntityValidationErrors) {
                    foreach (var validationError in validationErrors.ValidationErrors) {
                        Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine("Save failed", ex);
            }
            return 0;
        }

        public virtual TEntity Find(int id) {
            return _context.Set<TEntity>().Find(id);
        }

        public void Refresh(TEntity entity) {
            _context.Entry(entity).Reload();
        }

        public virtual int Count() {
            return _context.Set<TEntity>().Count();
        }

        public void Dispose() {
            if (_context != null)
                _context.Dispose();
        }
    }
}

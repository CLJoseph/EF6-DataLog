

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleEF6_DataLog
{
    public interface IRepository<T> where T : class
    {
        T Find(Expression<Func<T, bool>> predicate);
        List<T> ListAll();
        List<T> ListRange(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }

    public class Repository<T>:IRepository<T> where T:class
    {
        protected readonly ApplicationDbContext _Context;
        public Repository(ApplicationDbContext DbContext)
        {
            _Context = DbContext;
        }
        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _Context.Set<T>().SingleOrDefault(predicate);
            //return _Context.Set<T>().Find("", )
        }
        public void Add(T entity)
        {
            _Context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            _Context.Entry<T>(entity).State = EntityState.Modified;
        }
        public List<T> ListAll()
        {
            return _Context.Set<T>().ToList();
        }
        public List<T> ListRange(Expression<Func<T, bool>> predicate)
        {
            return _Context.Set<T>().Where(predicate).ToList();
        }
        public void Remove(T entity)
        {
            _Context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _Context.Set<T>().RemoveRange(entities);
        }
    }
}

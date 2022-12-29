using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        protected RepositoryContext repositoryContext;

        protected RepositoryBase(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        //trackChanges:improve our read-only query performance.
        //When it’s set to false, we attach the AsNoTracking method to inform EF Core that it doesn’t need to track changes for the required entities.This greatlyimproves the speed of a query
        public IQueryable<T> FiindAll() =>
          repositoryContext.Set<T>() ;

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
        repositoryContext.Set<T>().Where(expression);

        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);

    }
}

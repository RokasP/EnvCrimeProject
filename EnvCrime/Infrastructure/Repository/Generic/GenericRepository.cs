using EnvCrime.Models;
using System.Linq.Expressions;

namespace EnvCrime.Infrastructure.Repository.Generic
{
    public class GenericRepository<T, U> : IRepository<T, U> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbCtx)
        {
            dbContext = dbCtx;
        }

        // CRUD-operationerna
        public T GetById(U id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return dbContext.Set<T>().AsQueryable();
        }

        public T Save(T entity)
        {
            BeforeSave(entity);
            if (entity == null /*entity.id == 0*/)
            {
                dbContext.Set<T>().Add(entity);
            }
            dbContext.SaveChanges();
            AfterSave(entity);
            return entity;
        }

        public void DeleteById(U id)
        {
            T entity = GetById(id);
            if (entity == null)
            {
                throw new Exception($"Kan ej hitta objektet med id = {id} för att ta bort det");
            }
            dbContext.Remove(entity);
        }

        public virtual void BeforeSave(T entity) { }

        public virtual void AfterSave(T entity) { }

        // avancerade sökningsoperationerna
        public IQueryable<T> Search(params Expression<Func<T, bool>>[] searchPredicates)
        {
            return GetAll().Where(BuildQuery(searchPredicates));
        }

        private static Expression<Func<T, bool>> BuildQuery<T>(params Expression<Func<T, bool>>[] searchPredicates)
        {
            Expression<Func<T, bool>> compundExpression = x => true; // "identitetsfunktion", behövs för att lagra filtreringskriterier
            foreach (var predicate in searchPredicates)
            {
                Expression.AndAlso(compundExpression, predicate);
            }
            return compundExpression;
        }
    }
}

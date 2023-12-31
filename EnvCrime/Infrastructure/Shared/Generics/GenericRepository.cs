﻿using EnvCrime.Models;
using LinqKit;
using System.Linq.Expressions;

namespace EnvCrime.Infrastructure.Shared.Generics
{
    /*
     * Skapa en ny service-klass för varje poco-klass du vill hantera i applikationen och låt servicen ärva ifrån den här generiska repo-klassen.
     * På så sätt får din service-klass tillgång till allmänna CRUD-metoder och du kan sedan även lägga till logik som är specifik för din poco-klass.
     
     * Service-klasserna får registreras som serivces i filen Program.cs (där finner du exempel på hur man gör) och kan efteråt injiceras till kontrollers,
     * vy-komponenter eller på andra ställen i kodbasen.
     */
    public class GenericRepository<T, U> : IRepository<T, U> where T : GenericEntity<U>
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
            if (GetById(entity.GetId()) == null)
            {
                BeforeInitialSave(entity);
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
            dbContext.SaveChanges();
        }

        protected virtual void BeforeInitialSave(T entity) { }

        protected virtual void BeforeSave(T entity) { }

        protected virtual void AfterSave(T entity) { }

        // avancerade sökningsoperationerna
        public IQueryable<T> Search(params Expression<Func<T, bool>>[] searchPredicates)
        {
            return GetAll().Where(BuildQuery(searchPredicates));
        }

        private static Expression<Func<T, bool>> BuildQuery<T>(params Expression<Func<T, bool>>[] searchPredicates)
        {
            Expression<Func<T, bool>> compoundExpression = PredicateBuilder.New<T>(true); // "identitetsfunktion", behövs för att lagra filtreringskriterier
            foreach (var predicate in searchPredicates)
            {
                compoundExpression = compoundExpression.And(predicate);
            }
            return compoundExpression;
        }
    }
}

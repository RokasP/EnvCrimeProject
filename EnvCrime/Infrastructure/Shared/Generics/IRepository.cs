using System.Linq.Expressions;

namespace EnvCrime.Infrastructure.Shared.Generics
{
    /*
	 * T står för entitetstyp, i nuläget är
	 * T = [Errand, Department, Employee, ErrandStatus, Sequence, Picture, Sample]
	 * 
	 * U står för id-typ på motsvarande entitetstyper: 
	 * U = [int, string, string, string, int, int, int]
	 * 
	 */
    public interface IRepository<T, U> where T : GenericEntity<U>
    {
        public T GetById(U id);
        public IQueryable<T> GetAll();
        public T Save(T entity);
        public void DeleteById(U id);
        public IQueryable<T> Search(params Expression<Func<T, bool>>[] searchPredicates);
    }
}

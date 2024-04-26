using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NetCoreWebApp.Utility;
using System.Linq.Expressions;

namespace NetCoreWebApp.Models
{
    public class Repository<T> : IRepository<T> where T : class

    {
        private readonly UygulamaDbContext _uygulamaDbContext; //dbContext'e repository içinden erişicez.
        internal DbSet<T> dbSet;

        public Repository(UygulamaDbContext uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
            this.dbSet = _uygulamaDbContext.Set<T>(); // dbSet =  _uygulamaDbContext.KitapTürleri
        } 
        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu = sorgu.Where(filtre);
            return sorgu.FirstOrDefault();

        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> sorgu = dbSet;
            return sorgu.ToList();
        }

        public void Sil(T entity)
        {
            dbSet.Remove(entity);
        }

        public void SilAralik(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }

  
}

using System;
using System.Linq;
using System.Threading.Tasks;
using Core.AppDbContext;
using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IDomainModel
    {
        protected readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public abstract IQueryable<T> Ordering(IQueryable<T> source, OrderingOption orderingOption);

        public async ValueTask<PagedResult<T>> Pagination(IQueryable<T> source, PaginationOption paginationOption = null)
        {
            if (paginationOption == null)
            {
                paginationOption = new PaginationOption();
            }

            int totalCount = source.Count();
            int lastPage = (int)Math.Ceiling(totalCount / (double)paginationOption.PageSize);
            if (paginationOption.CurrentPage > lastPage)
            {
                paginationOption.CurrentPage = lastPage;
            }

            var getResult = source
                .Skip((paginationOption.CurrentPage - 1) * paginationOption.PageSize)
                .Take(paginationOption.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResult<T>(
                paginationOption.PageSize,
                paginationOption.CurrentPage,
                lastPage,
                totalCount,
                await getResult
            );
        }

        public IQueryable<T> RetrieveAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public ValueTask<T> RetrieveById(params object[] ids)
        {
            return _context.Set<T>().FindAsync(ids);
        }

        public abstract IQueryable<T> Searching();

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
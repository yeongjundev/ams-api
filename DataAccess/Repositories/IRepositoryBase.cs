using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace DataAccess.Repositories
{
    public interface IRepositoryBase<T> where T : class, IDomainModel
    {
        void Create(T entity);

        void Delete(T entity);

        IQueryable<T> Ordering(IQueryable<T> source, OrderingOption orderingOption);

        ValueTask<PagedResult<T>> Pagination(IQueryable<T> source, PaginationOption paginationOption);

        IQueryable<T> RetrieveAll();

        ValueTask<T> RetrieveById(params object[] ids);

        IQueryable<T> Searching(IQueryable<T> source, SearchingOption searchingOption);

        void Update(T entity);
    }
}
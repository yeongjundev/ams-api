using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace DataAccess.Repositories
{
    public interface IRepositoryBase<T> where T : class, IDomainModel
    {
        void Create(T entity);

        void CreateRange(List<T> entities);

        void Delete(T entity);

        void DeleteRange(List<T> entities);

        ValueTask<PagedResult<T>> Pagination(IQueryable<T> source, PaginationOption paginationOption);

        IQueryable<T> RetrieveAll();

        ValueTask<T> RetrieveById(params object[] ids);

        void Update(T entity);
    }
}
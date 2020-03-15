using System.Collections.Generic;
using Core.Models;

namespace Core.Helpers
{
    public class PagedResult<T> where T : class, IDomainModel
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int TotalCount { get; set; }

        public List<T> Result { get; set; }

        public PagedResult(int pageSize, int currentPage, int lastPage, int totalCount, List<T> result)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;
            LastPage = lastPage;
            TotalCount = totalCount;
            Result = result;
        }
    }
}
using System.Collections.Generic;
using Core.Models;

namespace Core.DTOs.Serializers
{
    public class PagedResultDTO<T> where T : class, IDomainModel
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int TotalCount { get; set; }

        public List<T> Result { get; set; }
    }
}
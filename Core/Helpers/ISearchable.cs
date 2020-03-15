using System.Linq;
using Core.Models;

namespace Core.Helpers
{
    public interface ISearchable<T> where T : class, IDomainModel
    {
        IQueryable<T> Searching(IQueryable<T> source, SearchingOption searchingOption);
    }
}
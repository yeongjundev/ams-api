using System.Linq;
using Core.Models;

namespace Core.Helpers
{
    public interface IOrderable<T> where T : class, IDomainModel
    {
        IQueryable<T> Ordering(IQueryable<T> source, OrderingOption orderingOption);
    }
}
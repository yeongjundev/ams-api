using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace DataAccess.Repositories
{
    public interface ILessonRepository : IRepositoryBase<Lesson>
    {
        ValueTask<PagedResult<Lesson>> RetrieveLessons(
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        );
    }
}
using System;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace DataAccess.Repositories
{
    public interface ILessonRepository : IRepositoryBase<Lesson>, IOrderable<Lesson>, ISearchable<Lesson>
    {
        ValueTask<PagedResult<Lesson>> RetrieveLessons(
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        );

        ValueTask<PagedResult<Lesson>> RetrieveEnrolledLessons(
            Guid studentId,
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        );
    }
}
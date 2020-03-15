using System;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace DataAccess.Repositories
{
    public interface IAttendanceSheetRepository : IRepositoryBase<AttendanceSheet>, IOrderable<AttendanceSheet>, ISearchable<AttendanceSheet>
    {
        ValueTask<AttendanceSheet> RetrieveById(Guid id);

        ValueTask<PagedResult<AttendanceSheet>> RetrieveAttendanceSheets(
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        );

        // ValueTask<PagedResult<AttendanceSheet>> RetrieveLessonAttendanceSheets(
        //     Guid lessonId,
        //     OrderingOption orderingOption,
        //     PaginationOption paginationOption
        // );
    }
}
using System;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace DataAccess.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>, IOrderable<Student>, ISearchable<Student>
    {
        ValueTask<PagedResult<Student>> RetrieveStudents(
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        );

        ValueTask<PagedResult<Student>> RetrieveEnrolledStudents(
            Guid lessonId,
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        );
    }
}
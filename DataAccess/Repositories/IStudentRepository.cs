using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace DataAccess.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Student>
    {
        ValueTask<PagedResult<Student>> RetrieveStudents(
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        );
    }
}
using System.Linq;
using System.Threading.Tasks;
using Core.AppDbContext;
using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context) { }

        public override IQueryable<Student> Ordering(
            IQueryable<Student> source,
            OrderingOption orderingOption
        )
        {
            if (orderingOption == null)
            {
                orderingOption = new OrderingOption();
            }

            switch (orderingOption.OrderBy.ToLower())
            {
                case "firstname":
                    return orderingOption.Desc ? source.OrderByDescending(source => source.Firstname) : source.OrderBy(source => source.Firstname);
                case "middlename":
                    return orderingOption.Desc ? source.OrderByDescending(source => source.Middlename) : source.OrderBy(source => source.Firstname);
                case "lastname":
                    return orderingOption.Desc ? source.OrderByDescending(source => source.Lastname) : source.OrderBy(source => source.Firstname);
                case "email":
                    return orderingOption.Desc ? source.OrderByDescending(source => source.Email) : source.OrderBy(source => source.Firstname);
                default:
                    return orderingOption.Desc ? source.OrderByDescending(source => source.CreateDateTime) : source.OrderBy(source => source.CreateDateTime);
            }
        }

        public ValueTask<PagedResult<Student>> RetrieveStudents(
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        )
        {
            var students = RetrieveAll();
            students = Searching(students, searchingOption);
            students = Ordering(students, orderingOption);
            return Pagination(students, paginationOption);
        }

        public override IQueryable<Student> Searching(
            IQueryable<Student> source,
            SearchingOption searchingOption
        )
        {
            if (searchingOption == null)
            {
                searchingOption = new SearchingOption();
            }

            if (searchingOption.SearchOn == null || searchingOption.Search == null)
            {
                return source;
            }

            var search = $"%{searchingOption.Search.ToLower()}%";
            switch (searchingOption.SearchOn.ToString())
            {
                case "firstname":
                    return source.Where(student => EF.Functions.Like(student.Firstname, search));
                case "middlename":
                    return source.Where(student => EF.Functions.Like(student.Middlename, search));
                case "lastname":
                    return source.Where(student => EF.Functions.Like(student.Lastname, search));
                case "email":
                    return source.Where(student => EF.Functions.Like(student.Email, search));
                case "phone":
                    return source.Where(student => EF.Functions.Like(student.Phone, search));
            }
            return source;
        }
    }
}
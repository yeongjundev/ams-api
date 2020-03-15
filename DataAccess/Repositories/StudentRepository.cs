using System.Collections.Generic;
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
            OrderingOption orderingOption,
            PaginationOption paginationOption
        )
        {
            var students = RetrieveAll();

            // searching

            // ordering
            students = Ordering(students, orderingOption);

            // Pagination
            return Pagination(students, paginationOption);
        }

        public override IQueryable<Student> Searching()
        {
            throw new System.NotImplementedException();
        }
    }
}
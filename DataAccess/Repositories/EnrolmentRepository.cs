using System.Linq;
using System.Threading.Tasks;
using Core.AppDbContext;
using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class EnrolmentRepository : RepositoryBase<Enrolment>, IEnrolmentRepository
    {
        public EnrolmentRepository(AppDbContext context) : base(context) { }

        // public override IQueryable<Enrolment> Ordering(
        //     IQueryable<Lesson> source,
        //     OrderingOption orderingOption
        // )
        // {
        //     if (orderingOption == null)
        //     {
        //         orderingOption = new OrderingOption();
        //     }

        //     switch (orderingOption.OrderBy.ToLower())
        //     {
        //         case "title":
        //             return orderingOption.Desc ? source.OrderByDescending(source => source.Title) : source.OrderBy(source => source.Title);
        //         default:
        //             return orderingOption.Desc ? source.OrderByDescending(source => source.CreateDateTime) : source.OrderBy(source => source.CreateDateTime);
        //     }
        // }

        // public ValueTask<PagedResult<Lesson>> RetrieveLessons(
        //     SearchingOption searchingOption,
        //     OrderingOption orderingOption,
        //     PaginationOption paginationOption
        // )
        // {
        //     var lessons = RetrieveAll();
        //     lessons = Searching(lessons, searchingOption);
        //     lessons = Ordering(lessons, orderingOption);
        //     return Pagination(lessons, paginationOption);
        // }

        // public override IQueryable<Lesson> Searching(
        //     IQueryable<Lesson> source,
        //     SearchingOption searchingOption
        // )
        // {
        //     if (searchingOption == null)
        //     {
        //         searchingOption = new SearchingOption();
        //     }

        //     if (searchingOption.SearchOn == null || searchingOption.Search == null)
        //     {
        //         return source;
        //     }

        //     var search = $"%{searchingOption.Search.ToLower()}%";
        //     switch (searchingOption.SearchOn.ToString())
        //     {
        //         case "title":
        //             return source.Where(lesson => EF.Functions.Like(lesson.Title, search));
        //     }
        //     return source;
        // }
    }
}
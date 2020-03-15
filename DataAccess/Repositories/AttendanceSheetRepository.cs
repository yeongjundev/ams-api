using System;
using System.Linq;
using System.Threading.Tasks;
using Core.AppDbContext;
using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AttendanceSheetRepository : RepositoryBase<AttendanceSheet>, IAttendanceSheetRepository
    {
        public AttendanceSheetRepository(AppDbContext context) : base(context) { }

        public override IQueryable<AttendanceSheet> RetrieveAll()
        {
            return base.RetrieveAll()
                .Include(attendanceSheet => attendanceSheet.Lesson)
                .Include(attendanceSheet => attendanceSheet.Attendances)
                .ThenInclude(attendance => attendance.Student);
        }

        public ValueTask<AttendanceSheet> RetrieveById(Guid id)
        {
            var attendanceSheet = RetrieveAll()
                .Include(attendanceSheet => attendanceSheet.Lesson)
                .Where(attendanceSheet => attendanceSheet.Id == id)
                .FirstAsync();
            return new ValueTask<AttendanceSheet>(attendanceSheet);
        }

        public IQueryable<AttendanceSheet> Ordering(
            IQueryable<AttendanceSheet> source,
            OrderingOption orderingOption
        )
        {
            if (orderingOption == null)
            {
                orderingOption = new OrderingOption();
            }

            switch (orderingOption.OrderBy.ToLower())
            {
                case "title":
                    return orderingOption.Desc ? source.OrderByDescending(source => source.Lesson.Title) : source.OrderBy(source => source.Lesson.Title);
                default:
                    return orderingOption.Desc ? source.OrderByDescending(source => source.CreateDateTime) : source.OrderBy(source => source.CreateDateTime);
            }
        }

        public ValueTask<PagedResult<AttendanceSheet>> RetrieveAttendanceSheets(
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        )
        {
            var attendanceSheets = RetrieveAll();
            attendanceSheets = Searching(attendanceSheets, searchingOption);
            attendanceSheets = Ordering(attendanceSheets, orderingOption);
            return Pagination(attendanceSheets, paginationOption);
        }

        public IQueryable<AttendanceSheet> Searching(
            IQueryable<AttendanceSheet> source,
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
                case "title":
                    return source.Where(attendanceSheet => EF.Functions.Like(attendanceSheet.Lesson.Title, search));
            }
            return source;
        }

        // public ValueTask<PagedResult<Lesson>> RetrieveEnrolledLessons(
        //     Guid studentId,
        //     SearchingOption searchingOption,
        //     OrderingOption orderingOption,
        //     PaginationOption paginationOption
        // )
        // {
        //     var enrolments = _context.Set<Enrolment>().AsQueryable();

        //     var enrolledLessons = enrolments
        //         .Include(enrolment => enrolment.Lesson)
        //         .Where(enrolment => enrolment.StudentId == studentId)
        //         .Select(enrolment => enrolment.Lesson);

        //     enrolledLessons = Searching(enrolledLessons, searchingOption);
        //     enrolledLessons = Ordering(enrolledLessons, orderingOption);
        //     return Pagination(enrolledLessons, paginationOption);
        // }
    }
}
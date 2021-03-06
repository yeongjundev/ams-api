using System;
using System.Linq;
using System.Threading.Tasks;
using Core.AppDbContext;
using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class LessonRepository : RepositoryBase<Lesson>, ILessonRepository
    {
        public LessonRepository(AppDbContext context) : base(context) { }

        public IQueryable<Lesson> Ordering(
            IQueryable<Lesson> source,
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
                    return orderingOption.Desc ? source.OrderByDescending(source => source.Title) : source.OrderBy(source => source.Title);
                default:
                    return orderingOption.Desc ? source.OrderByDescending(source => source.CreateDateTime) : source.OrderBy(source => source.CreateDateTime);
            }
        }

        public ValueTask<PagedResult<Lesson>> RetrieveLessons(
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        )
        {
            var lessons = RetrieveAll();
            lessons = Searching(lessons, searchingOption);
            lessons = Ordering(lessons, orderingOption);
            return Pagination(lessons, paginationOption);
        }

        public IQueryable<Lesson> Searching(
            IQueryable<Lesson> source,
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
                    return source.Where(lesson => EF.Functions.Like(lesson.Title, search));
            }
            return source;
        }

        public ValueTask<PagedResult<Lesson>> RetrieveEnrolledLessons(
            Guid studentId,
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        )
        {
            var enrolments = _context.Set<Enrolment>().AsQueryable();

            var enrolledLessons = enrolments
                .Include(enrolment => enrolment.Lesson)
                .Where(enrolment => enrolment.StudentId == studentId)
                .Select(enrolment => enrolment.Lesson);

            enrolledLessons = Searching(enrolledLessons, searchingOption);
            enrolledLessons = Ordering(enrolledLessons, orderingOption);
            return Pagination(enrolledLessons, paginationOption);
        }
    }
}
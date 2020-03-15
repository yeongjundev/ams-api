using System;
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

        public IQueryable<Student> Ordering(
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

        public IQueryable<Student> Searching(
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

        public ValueTask<PagedResult<Student>> RetrieveEnrolledStudents(
            Guid lessonId,
            SearchingOption searchingOption,
            OrderingOption orderingOption,
            PaginationOption paginationOption
        )
        {
            var enrolments = _context.Set<Enrolment>().AsQueryable();

            var enrolledStudents = enrolments
                .Include(enrolment => enrolment.Student)
                .Where(enrolment => enrolment.LessonId == lessonId)
                .Select(enrolment => enrolment.Student);

            enrolledStudents = Searching(enrolledStudents, searchingOption);
            enrolledStudents = Ordering(enrolledStudents, orderingOption);
            return Pagination(enrolledStudents, paginationOption);
        }

        public ValueTask<List<Student>> RetrieveEnrolledStudents(Guid lessonId)
        {
            var enrolments = _context.Set<Enrolment>().AsQueryable();

            return new ValueTask<List<Student>>(
                enrolments
                    .Include(enrolment => enrolment.Student)
                    .Where(enrolment => enrolment.LessonId == lessonId)
                    .Select(enrolment => enrolment.Student)
                    .ToListAsync()
            );
        }
    }
}
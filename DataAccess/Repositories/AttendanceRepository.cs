using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.AppDbContext;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class AttendanceRepository : RepositoryBase<Attendance>, IAttendanceRepository
    {
        public AttendanceRepository(AppDbContext context) : base(context) { }

        public ValueTask<List<Attendance>> RetrieveAttendanceSheetAttendances(Guid attendanceSheetId)
        {
            var attendances = RetrieveAll()
                .Include(attendance => attendance.Student)
                .Include(attendance => attendance.Lesson)
                .Where(attendance => attendance.AttendanceSheetId == attendanceSheetId)
                .ToListAsync();
            return new ValueTask<List<Attendance>>(attendances);
        }

        public ValueTask<List<Attendance>> RetrieveStudentAttendances(Guid studentId)
        {
            var attendances = RetrieveAll()
                .Include(attendance => attendance.AttendanceSheet)
                // .Include(attendance => attendance.Lesson)
                .Where(attendance => attendance.StudentId == studentId)
                .ToListAsync();
            return new ValueTask<List<Attendance>>(attendances);
        }
    }
}
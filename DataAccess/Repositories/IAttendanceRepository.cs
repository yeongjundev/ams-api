using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Models;

namespace DataAccess.Repositories
{
    public interface IAttendanceRepository : IRepositoryBase<Attendance>
    {
        ValueTask<List<Attendance>> RetrieveAttendanceSheetAttendances(Guid attendanceSheetId);

        ValueTask<List<Attendance>> RetrieveStudentAttendances(Guid studentId);
    }
}
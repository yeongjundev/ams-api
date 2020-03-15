using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;
using DataAccess.UnitOfWork;

namespace Services.AttendanceManager
{
    public class AttendanceManager : IAttendanceManager
    {
        private readonly IUnitOfWork _uow;

        public AttendanceManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async ValueTask<AttendanceSheet> CreateAttendanceSheet(AttendanceSheet newAttendanceSheet)
        {
            _uow.AttendanceSheetRepository.Create(newAttendanceSheet);

            // Create attendances for enrolled students.
            var enrolledStudents = _uow.StudentRepository.RetrieveEnrolledStudents(newAttendanceSheet.LessonId);
            newAttendanceSheet.Attendances = new List<Attendance>();
            foreach (var student in await enrolledStudents)
            {
                newAttendanceSheet.Attendances.Add(
                    new Attendance
                    {
                        AttendanceSheetId = newAttendanceSheet.Id,
                        LessonId = newAttendanceSheet.LessonId,
                        StudentId = student.Id,
                        Comment = "",
                        AttendanceType = AttendanceType.Absent
                    }
                );
            }
            return newAttendanceSheet;
        }
    }
}
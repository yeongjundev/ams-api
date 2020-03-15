using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }

        ILessonRepository LessonRepository { get; }

        IEnrolmentRepository EnrolmentRepository { get; }

        IAttendanceSheetRepository AttendanceSheetRepository { get; }

        public IAttendanceRepository AttendanceRepository { get; }

        int Complete(bool ignoreResult);
    }
}
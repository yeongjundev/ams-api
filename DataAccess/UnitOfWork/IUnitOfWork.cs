using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }

        ILessonRepository LessonRepository { get; }

        IEnrolmentRepository EnrolmentRepository { get; }

        int Complete(bool ignoreResult);
    }
}
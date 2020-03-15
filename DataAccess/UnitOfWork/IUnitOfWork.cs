using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }

        ILessonRepository LessonRepository { get; }

        int Complete(bool ignoreResult);
    }
}
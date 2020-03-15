using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }

        int Complete(bool ignoreResult);
    }
}
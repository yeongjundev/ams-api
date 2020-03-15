using System;
using System.Threading.Tasks;
using Core.AppDbContext;
using DataAccess.Repositories;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IStudentRepository _studentRepository;

        public IStudentRepository StudentRepository
        {
            get
            {
                if (_studentRepository == null)
                {
                    _studentRepository = new StudentRepository(_context);
                }
                return _studentRepository;
            }
        }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public int Complete(bool ignoreResult)
        {
            int result = _context.SaveChanges();
            if (!ignoreResult && result == 0)
            {
                throw new InvalidOperationException($"`UnitOfWork`, `Complete()`: SaveChanges reutrns 0 when ignoreResult is false.");
            }
            return result;
        }
    }
}
using System;
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

        private ILessonRepository _lessonRepository;

        public ILessonRepository LessonRepository
        {
            get
            {
                if (_lessonRepository == null)
                {
                    _lessonRepository = new LessonRepository(_context);
                }
                return _lessonRepository;
            }
        }

        private IEnrolmentRepository _enrolmentRepository;

        public IEnrolmentRepository EnrolmentRepository
        {
            get
            {
                if (_enrolmentRepository == null)
                {
                    _enrolmentRepository = new EnrolmentRepository(_context);
                }
                return _enrolmentRepository;
            }
        }

        private IAttendanceSheetRepository _attendanceSheetRepository;

        public IAttendanceSheetRepository AttendanceSheetRepository
        {
            get
            {
                if (_attendanceSheetRepository == null)
                {
                    _attendanceSheetRepository = new AttendanceSheetRepository(_context);
                }
                return _attendanceSheetRepository;
            }
        }

        private IAttendanceRepository _attendanceRepository;

        public IAttendanceRepository AttendanceRepository
        {
            get
            {
                if (_attendanceRepository == null)
                {
                    _attendanceRepository = new AttendanceRepository(_context);
                }
                return _attendanceRepository;
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
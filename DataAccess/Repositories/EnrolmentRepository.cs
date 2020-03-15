using System;
using System.Linq;
using System.Threading.Tasks;
using Core.AppDbContext;
using Core.Helpers;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class EnrolmentRepository : RepositoryBase<Enrolment>, IEnrolmentRepository
    {
        public EnrolmentRepository(AppDbContext context) : base(context) { }
    }
}
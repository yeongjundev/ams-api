using System.Threading.Tasks;
using Core.Models;

namespace Services.AttendanceManager
{
    public interface IAttendanceManager
    {
        ValueTask<AttendanceSheet> CreateAttendanceSheet(AttendanceSheet newAttendanceSheet);
    }
}
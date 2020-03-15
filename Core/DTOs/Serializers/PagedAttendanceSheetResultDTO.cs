using System.Collections.Generic;
using Core.Models;

namespace Core.DTOs.Serializers
{
    public class PagedAttendanceSheetDTO
    {
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public int LastPage { get; set; }

        public int TotalCount { get; set; }

        public List<AttendanceSheetDTO> Result { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace AttendanceTracker.Models.DTO
{
   
    public class ClassSessionDto
    {
        public ClassSessionDto()
        {
            StudentIds = new HashSet<int>();
        }
        
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? StudentClassId { get; set; }
        public IEnumerable<int> StudentIds { get; set; }
    }
}
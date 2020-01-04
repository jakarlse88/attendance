using System;
using System.Collections.Generic;

namespace AttendanceTracker.Models.DTO
{
   
    public class ClassSessionDto
    {
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int StudentClassId { get; set; }
        public HashSet<int> StudentIds { get; set; }
    }
}
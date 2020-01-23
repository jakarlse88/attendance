using System;
using System.Collections.Generic;

namespace AttendanceTracker.Models
{
    public class ClassSession : EntityBase
    {
        public ClassSession()
        {
            Students = new HashSet<Student>();
        }
        
        public override int Id { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public StudentClass StudentClass { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
using System.Collections.Generic;

namespace AttendanceTracker.Models
{
    public class ClassSession
    {
        public int Id { get; set; }
        public StudentClass StudentClass { get; set; }
        public HashSet<Student> Students { get; set; }
    }
}
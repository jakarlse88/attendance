using System;

namespace AttendanceTracker.Models
{
    public class StudentClass : EntityBase
    {
        public override int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
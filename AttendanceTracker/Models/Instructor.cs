using System.Collections.Generic;

namespace AttendanceTracker.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        
        public HashSet<StudentClass> ClassesTaught { get; set; }
    }
}
namespace AttendanceTracker.Models
{
    public class Student : EntityBase
    {
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Grade Grade { get; set; }
        public StudentClass StudentClass { get; set; }
    }
}
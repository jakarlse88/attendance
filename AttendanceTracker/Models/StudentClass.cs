namespace AttendanceTracker.Models
{
    public class StudentClass : EntityBase
    {
        public override int Id { get; set; }
        public string Description { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
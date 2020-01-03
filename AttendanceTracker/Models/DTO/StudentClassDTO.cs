namespace AttendanceTracker.Models.DTO
{
    public class StudentClassDto
    {
        public string Description { get; set; }

        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
    }
}
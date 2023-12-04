namespace Application.DTOs.StudentDTOs
{
    public class ViewStudentDTO
    {
        public int StudentId { get; set; }
        public string StudentName111 { get; set; }
        public string StudentAge { get; set; }
        public string? CreatedDetails { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public bool IsActive { get; set; }

        // The field which is not in source is given default value
        public bool StudentDead { get; set; }
    }
}

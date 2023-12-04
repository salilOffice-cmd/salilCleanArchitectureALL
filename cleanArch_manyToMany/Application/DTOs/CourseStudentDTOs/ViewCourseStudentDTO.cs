using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CourseStudentDTOs
{
    public class ViewCourseStudentDTO
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string StudentAge { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}

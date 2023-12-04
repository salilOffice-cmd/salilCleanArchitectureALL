using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.StudentDTOs
{
    public class AddStudentDTO
    {
        public string StudentName { get; set; }
        public string StudentAge { get; set; }
        public int CourseID { get; set; }
    }
}

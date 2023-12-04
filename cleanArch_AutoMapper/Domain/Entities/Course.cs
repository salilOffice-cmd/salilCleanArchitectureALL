using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Course : Auditable
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public List<Student> Students { get; set; }
    }
}

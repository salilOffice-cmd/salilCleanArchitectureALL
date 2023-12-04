using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : Auditable
    {
        [Key]
        public int StudentID{ get; set; }
        public string StudentName { get; set; }
        public string StudentAge { get; set;}

        [ForeignKey("Course")]
        public int CourseID { get; set;}

        public Course Course { get; set;}
    }
}

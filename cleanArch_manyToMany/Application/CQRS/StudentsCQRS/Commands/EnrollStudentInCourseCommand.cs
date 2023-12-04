using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.StudentsCQRS.Commands
{
    public class EnrollStudentInCourseCommand : IRequest<string>
    {
        public int StudentID { get; set; }
        public int CourseID { get; set; }
    }


    public class EnrollStudentInCourseCommand_Handler : IRequestHandler<EnrollStudentInCourseCommand, string>
    {
        private readonly IApplicationDBContext context;

        public EnrollStudentInCourseCommand_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<string> Handle(EnrollStudentInCourseCommand request, CancellationToken cancellationToken)
        {
            var courseTable = await context.Courses.ToListAsync();
            var studentTable = await context.Students.ToListAsync();

            var foundCourse = courseTable.Where(c => c.CourseId == request.CourseID)
                                         .FirstOrDefault();

            var foundStudent = studentTable.Where(s => s.StudentID == request.StudentID)
                                         .FirstOrDefault();

            if (foundCourse != null)
            {
                if (foundStudent != null)
                {
                    CourseStudent courseStudent = new CourseStudent
                    {
                        Course = foundCourse,
                        Student = foundStudent
                    };

                    await context.CourseStudents.AddAsync(courseStudent);
                    int rowsAffected = await context.SaveChangesAsync(cancellationToken);

                    if(rowsAffected > 0)
                    {
                        return "Student Enrolled Successfully!";
                    }

                    return "No changes were saved to the database!";
                }
                else
                {
                    return "Student with the given Id not found!";
                }
            }

            else
            {
                return "Course with the given Id not found!";
            }

        }
    }
}

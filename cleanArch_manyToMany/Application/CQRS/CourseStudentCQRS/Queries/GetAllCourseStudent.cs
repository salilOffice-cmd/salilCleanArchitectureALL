using Application.DTOs.CourseStudentDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.CourseStudentCQRS.Queries
{
    public class GetAllCourseStudent : IRequest<List<ViewCourseStudentDTO>>
    {

    }

    public class GetAllCourseStudent_Handler : IRequestHandler<GetAllCourseStudent, List<ViewCourseStudentDTO>>
    {
        private readonly IApplicationDBContext context;

        public GetAllCourseStudent_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }
        public async Task<List<ViewCourseStudentDTO>> Handle(GetAllCourseStudent request, CancellationToken cancellationToken)
        {
            var courseTable = await context.Courses.ToListAsync();
            var studentTable = await context.Students.ToListAsync();
            var courseStudentTable = await context.CourseStudents.ToListAsync();

            var join_CourseStudent_to_Course = courseStudentTable.Join(
                    courseTable,
                    cs => cs.CourseId,
                    course => course.CourseId,
                    (cs, course) => new
                    {
                        cs.CourseId,
                        cs.StudentId,
                        course.CourseName
                    }
                ).ToList();


            var join_above_JoinedTable_to_Student = join_CourseStudent_to_Course.Join(
                studentTable,
                aboveJoinedTable => aboveJoinedTable.StudentId,
                student => student.StudentID,
                (aboveJoinedTable, student) => new ViewCourseStudentDTO
                {
                    StudentID = student.StudentID,
                    StudentName = student.StudentName,
                    StudentAge = student.StudentAge,
                    CourseId = aboveJoinedTable.CourseId,
                    CourseName = aboveJoinedTable.CourseName,
                }).ToList();

            return join_above_JoinedTable_to_Student;
        }
    }

}

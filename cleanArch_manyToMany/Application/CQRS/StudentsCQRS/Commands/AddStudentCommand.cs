using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.StudentDTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.StudentsCQRS.Commands
{

    public class AddStudentCommand : IRequest<Student>
    {
        public AddStudentDTO addStudentDTO { get; set; }
    }


    public class AddStudentCommand_Handler : IRequestHandler<AddStudentCommand, Student>
    {
        private readonly IApplicationDBContext context;
        public AddStudentCommand_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<Student> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var addStudentDTO = request.addStudentDTO;

            var foundCourse = await context.Courses
                                .Where(c => c.CourseId == addStudentDTO.CourseId)
                                .FirstOrDefaultAsync();


            Student newStudent = new Student
            {
                StudentName = addStudentDTO.StudentName,
                StudentAge = addStudentDTO.StudentAge,
                CreatedDate = DateTime.Now,
                CreatedBy = "Admin",
                IsActive = true
            };


            CourseStudent courseStudent = new CourseStudent
            {
                Course = foundCourse,
                Student = newStudent
            };

            context.CourseStudents.Add(courseStudent);
            context.Students.Add(newStudent);
            await context.SaveChangesAsync(cancellationToken);

            return newStudent;

        }
    }

    public class AddStudentCommand_Handler2 : IRequestHandler<AddStudentCommand, Student>
    {
        private readonly IApplicationDBContext context;
        public AddStudentCommand_Handler2(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<Student> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var addStudentDTO = request.addStudentDTO;

            Student newStudent = new Student
            {
                StudentName = addStudentDTO.StudentName,
                StudentAge = addStudentDTO.StudentAge,
                CreatedDate = DateTime.Now,
                CreatedBy = "Admin",
                IsActive = true
            };


            context.Students.Add(newStudent);
            await context.SaveChangesAsync(cancellationToken);

            return newStudent;

        }
    }

}

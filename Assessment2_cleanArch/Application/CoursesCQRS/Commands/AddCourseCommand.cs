using Application.DTOs.CourseDTOs;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Commands
{
    public class AddCourseCommand : IRequest<Course>
    {
        public AddCourseDTO addCourseDTO { get; set; }
    }


    public class AddCourseCommand_Handler : IRequestHandler<AddCourseCommand, Course>
    {
        private readonly IApplicationDBContext context;
        public AddCourseCommand_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<Course> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {

            Course newCourse = new Course
            {
                CourseName = request.addCourseDTO.CourseName,
                CreatedDate = DateTime.Now,
                CreatedBy = "Admin",
                IsActive = true
            };


            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            return newCourse;

        }
    }
}

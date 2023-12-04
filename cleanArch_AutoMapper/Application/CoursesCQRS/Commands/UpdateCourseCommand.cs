using Application.DTOs.CourseDTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Commands
{
    public class UpdateCourseCommand : IRequest<string>
    {
        public UpdateCourseDTO UpdateCourseDTO { get; set; }
    }

    public class UpdateCourseCommand_Handler : IRequestHandler<UpdateCourseCommand, string>
    {
        private readonly IApplicationDBContext context;

        public UpdateCourseCommand_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<string> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var updateCourseDTO = request.UpdateCourseDTO;

            var gotCoursesTable = await context.Courses.AsNoTracking().ToListAsync();

            var gotCourse = gotCoursesTable
                            .FirstOrDefault(c => c.CourseId == updateCourseDTO.CourseId);

            if (gotCourse != null)
            {
                if (gotCourse.IsActive == false) return "Course Not found";

                Course updatedCourseObject = new Course
                {
                    CourseId = updateCourseDTO.CourseId,
                    CourseName = updateCourseDTO.CourseName,
                    CreatedBy = gotCourse.CreatedBy,
                    CreatedDate = gotCourse.CreatedDate,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedBy = "Admin",
                    IsActive = gotCourse.IsActive
                };


                context.Courses.Update(updatedCourseObject);
                await context.SaveChangesAsync(cancellationToken);

                return "Course Updated";

            }

            return "Course with the given ID not found";
        }
    }
}

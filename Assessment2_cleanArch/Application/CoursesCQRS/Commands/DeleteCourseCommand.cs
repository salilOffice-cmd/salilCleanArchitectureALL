using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Commands
{
    public class DeleteCourseCommand : IRequest<string>
    {
        public int CourseID { get; set; }
    }


    public class DeleteCourseCommand_Handler : IRequestHandler<DeleteCourseCommand, string>
    {
        private readonly IApplicationDBContext context;

        public DeleteCourseCommand_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<string> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var gotCoursesTable = await context.Courses.ToListAsync();

            var gotCourse = gotCoursesTable
                            .FirstOrDefault( c => c.CourseId == request.CourseID);


            if (gotCourse != null)
            {
                if (gotCourse.IsActive == false) return "Course already deleted";

                gotCourse.IsActive = false;
                gotCourse.LastModifiedDate = DateTime.Now;
                gotCourse.LastModifiedBy = "Admin";
                await context.SaveChangesAsync(cancellationToken);
                return "Course Deleted";
            }

            return "Course with the given ID not found";
        }
    }
}

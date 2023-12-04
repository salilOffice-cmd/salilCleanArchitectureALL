using Application.DTOs.CourseDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Queries
{
    public class GetCourseByIDQuery : IRequest<ViewCourseDTO>
    {
        public int CourseID { get; set; }   
    }

    public class GetCourseByIDQuery_Handler : IRequestHandler<GetCourseByIDQuery, ViewCourseDTO>
    {
        private readonly IApplicationDBContext context;
        public GetCourseByIDQuery_Handler(IApplicationDBContext _context)
        {
            context = _context;
        }

        public async Task<ViewCourseDTO> Handle(GetCourseByIDQuery request, CancellationToken cancellationToken)
        {
            var gotCourse = await context.Courses
                            .FirstOrDefaultAsync(c => c.CourseId == request.CourseID);

            if (gotCourse != null)
            {
                ViewCourseDTO viewCourseDTO = new ViewCourseDTO
                {
                    CourseId = gotCourse.CourseId,
                    CourseName = gotCourse.CourseName,
                    CreatedBy = gotCourse.CreatedBy,
                    CreatedDate = gotCourse.CreatedDate,
                    LastModifiedDate = gotCourse.LastModifiedDate,
                    LastModifiedBy = gotCourse.LastModifiedBy,
                    IsActive = gotCourse.IsActive
                };

                return viewCourseDTO;
            }

            return null;
        }
    }
}

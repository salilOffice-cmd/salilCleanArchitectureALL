using Application.DTOs.CourseDTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CoursesCQRS.Queries
{
    public class GetAllCoursesQuery : IRequest<List<ViewCourseDTO>>
    {
        public int limitCount { get; set; }
    }

    public class GetAllCoursesQuery_Handler : IRequestHandler<GetAllCoursesQuery, List<ViewCourseDTO>>
    {
        private readonly IApplicationDBContext context;
        public GetAllCoursesQuery_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<List<ViewCourseDTO>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            List<Course> gotAllCourses;
            if (request.limitCount > 0)
            {
                 gotAllCourses = await context.Courses.Take(request.limitCount).ToListAsync();
            }

            else
            {
                gotAllCourses = await context.Courses.ToListAsync();
            }

            var allCoursesDTO = gotAllCourses.Select(c => new ViewCourseDTO
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                LastModifiedDate = c.LastModifiedDate,
                LastModifiedBy = c.LastModifiedBy,
                IsActive = c.IsActive

            }).ToList();

            return allCoursesDTO;
        }
    }
}

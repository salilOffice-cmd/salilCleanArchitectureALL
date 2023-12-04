using Application.CQRS.CourseStudentCQRS.Queries;
using Application.DTOs.CourseDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.CoursesCQRS.Queries
{

    public class GetCoursesByStudentIDQuery : IRequest<GetCoursesByStudentIdDTO>
    {
        public int StudentID { get; set; }
    }

    public class GetCoursesByStudentIDQuery_Handler : IRequestHandler<GetCoursesByStudentIDQuery, GetCoursesByStudentIdDTO>
    {
        private readonly IApplicationDBContext context;
        private readonly IMediator mediator;
        public GetCoursesByStudentIDQuery_Handler(IApplicationDBContext _applicationDBContext,
                                                  IMediator _mediator)
        {
            context = _applicationDBContext;
            mediator = _mediator;
        }

        public async Task<GetCoursesByStudentIdDTO> Handle(GetCoursesByStudentIDQuery request, CancellationToken cancellationToken)
        {


            var gotStudent = await context.Students
                                   .FirstOrDefaultAsync(s => s.StudentID == request.StudentID);

            var joinsList = await mediator.Send(new GetAllCourseStudent { });

            var filteredJoinsList = joinsList.Where(cs => cs.StudentID == gotStudent.StudentID)
                                             .ToList();

            GetCoursesByStudentIdDTO getCoursesByStudentIdDTO = new GetCoursesByStudentIdDTO
            {
                StudentID = gotStudent.StudentID,
                StudentName = gotStudent.StudentName,
                StudentAge = gotStudent.StudentAge,
                Courses = filteredJoinsList.Select( cs => new ViewCourseDTO
                {
                    CourseId = cs.CourseId,
                    CourseName = cs.CourseName

                }).ToList()
            };

            return getCoursesByStudentIdDTO;

        }
    }
}

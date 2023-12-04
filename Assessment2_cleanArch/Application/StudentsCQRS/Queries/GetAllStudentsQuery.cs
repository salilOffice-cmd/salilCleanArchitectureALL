using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StudentsCQRS.Queries
{
    public class GetAllStudentsQuery : IRequest<List<ViewStudentDTO>>
    {

    }

    public class GetAllStudentsQuery_Handler : IRequestHandler<GetAllStudentsQuery, List<ViewStudentDTO>>
    {
        private readonly IApplicationDBContext context;
        public GetAllStudentsQuery_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<List<ViewStudentDTO>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var gotAllStudents = await context.Students.ToListAsync();
            var allStudentsDTO = gotAllStudents.Select(s => new ViewStudentDTO
            {
                StudentID = s.StudentID,
                StudentName = s.StudentName,
                StudentAge = s.StudentAge,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                LastModifiedDate = s.LastModifiedDate,
                LastModifiedBy = s.LastModifiedBy,
                IsActive = s.IsActive

            }).ToList();

            return allStudentsDTO;
        }
    }
}

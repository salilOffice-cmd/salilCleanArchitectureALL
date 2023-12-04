using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using AutoMapper;
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
        private readonly IMapper mapper;

        public GetAllStudentsQuery_Handler(IApplicationDBContext _applicationDBContext, IMapper _mapper)
        {
            context = _applicationDBContext;
            mapper = _mapper;
        }

        public async Task<List<ViewStudentDTO>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var gotAllStudents = await context.Students.ToListAsync();
            
            // AutoMapper changes -->
            // Now i can comment this
            //var allStudentsDTO = gotAllStudents.Select(s => new ViewStudentDTO
            //{
            //    StudentID = s.StudentID,
            //    StudentName = s.StudentName,
            //    StudentAge = s.StudentAge,
            //    CreatedBy = s.CreatedBy,
            //    CreatedDate = s.CreatedDate,
            //    LastModifiedDate = s.LastModifiedDate,
            //    LastModifiedBy = s.LastModifiedBy,
            //    IsActive = s.IsActive

            //}).ToList();

            var allStudentsDTO = mapper.Map<List<ViewStudentDTO>>(gotAllStudents);



            return allStudentsDTO;
        }
    }
}

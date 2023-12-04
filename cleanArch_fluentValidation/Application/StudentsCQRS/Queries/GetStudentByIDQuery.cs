using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StudentsCQRS.Queries
{
    public class GetStudentByIDQuery : IRequest<ViewStudentDTO>
    {
        public int studentID { get; set; }
    }

    public class GetStudentByIDQuery_Handler : IRequestHandler<GetStudentByIDQuery, ViewStudentDTO>
    {
        private readonly IApplicationDBContext context;
        public GetStudentByIDQuery_Handler(IApplicationDBContext _context)
        {
            context = _context;
        }

        public async Task<ViewStudentDTO> Handle(GetStudentByIDQuery request, CancellationToken cancellationToken)
        {
            var gotStudent = await context.Students
                            .FirstOrDefaultAsync(s => s.StudentID == request.studentID);

            if (gotStudent != null)
            {
                ViewStudentDTO viewStudentDTO = new ViewStudentDTO
                {
                    StudentID = gotStudent.StudentID,
                    StudentName = gotStudent.StudentName,
                    StudentAge = gotStudent.StudentAge, 
                    CreatedBy = gotStudent.CreatedBy,
                    CreatedDate = gotStudent.CreatedDate,
                    LastModifiedDate = gotStudent.LastModifiedDate,
                    LastModifiedBy = gotStudent.LastModifiedBy,
                    IsActive = gotStudent.IsActive
                };

                return viewStudentDTO;
            }

            return null;
        }
    }
}

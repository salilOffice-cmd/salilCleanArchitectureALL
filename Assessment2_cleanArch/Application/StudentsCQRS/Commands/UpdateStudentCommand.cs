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

namespace Application.StudentsCQRS.Commands
{
    public class UpdateStudentCommand : IRequest<string>
    {
        public UpdateStudentDTO UpdateStudentDTO { get; set; }
    }

    public class UpdateStudentCommand_Handler : IRequestHandler<UpdateStudentCommand, string>
    {
        private readonly IApplicationDBContext context;

        public UpdateStudentCommand_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<string> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var updateStudentDTO = request.UpdateStudentDTO;

            var gotStudentTable = await context.Students.AsNoTracking().ToListAsync();

            var gotStudent = gotStudentTable
                            .FirstOrDefault(s => s.StudentID == updateStudentDTO.StudentID);

            if (gotStudent != null)
            {
                if (gotStudent.IsActive == false) return "Student Not found";

                Student updatedStudentObject = new Student
                {
                    StudentID = updateStudentDTO.StudentID,
                    StudentName = updateStudentDTO.StudentName,
                    StudentAge = updateStudentDTO.StudentAge,
                    CourseID = updateStudentDTO.CourseID,
                    CreatedBy = gotStudent.CreatedBy,
                    CreatedDate = gotStudent.CreatedDate,
                    LastModifiedDate = DateTime.Now,
                    LastModifiedBy = "Admin",
                    IsActive = gotStudent.IsActive
                };


                context.Students.Update(updatedStudentObject);
                await context.SaveChangesAsync(cancellationToken);

                return "Course Updated";

            }

            return "Course with the given ID not found";
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StudentsCQRS.Commands
{
    public class DeleteStudentCommand : IRequest<string>
    {
        public int StudentID { get; set; }
    }


    public class DeleteStudentCommand_Handler : IRequestHandler<DeleteStudentCommand, string>
    {
        private readonly IApplicationDBContext context;

        public DeleteStudentCommand_Handler(IApplicationDBContext _applicationDBContext)
        {
            context = _applicationDBContext;
        }

        public async Task<string> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var gotStudentsTable = await context.Students.ToListAsync();

            var gotStudent = gotStudentsTable
                            .FirstOrDefault(s => s.StudentID == request.StudentID);

            if (gotStudent != null)
            {
                if (gotStudent.IsActive == false) return "Student already deleted";

                gotStudent.IsActive = false;
                gotStudent.LastModifiedDate = DateTime.Now;
                gotStudent.LastModifiedBy = "Admin";
                await context.SaveChangesAsync(cancellationToken);
                return "Student Deleted";
            }

            return "Student with the given ID not found";
        }
    }
}

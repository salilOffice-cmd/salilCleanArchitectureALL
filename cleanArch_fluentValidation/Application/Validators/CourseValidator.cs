using Application.DTOs.CourseDTOs;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    // For all validator methods
    // visit https://docs.fluentvalidation.net/en/latest/built-in-validators.html
    public class CourseValidator : AbstractValidator<AddCourseDTO>
    {
        //public CourseValidator()
        //{
        //    RuleFor(c => c.CourseName)
        //        .NotEmpty().WithMessage("Coursename should not be empty or null")
        //        .Length(3, 50).WithMessage("Coursename must be 3 to 50 characters long");
        //}


        private readonly IApplicationDBContext dbContext;
        public CourseValidator(IApplicationDBContext _dbContext)
        {
            dbContext = _dbContext;

            RuleFor(c => c.CourseName)
                .NotEmpty().WithMessage("Coursename should not be empty or null")
                .Length(3, 50).WithMessage("Coursename must be 3 to 50 characters long")
                .CustomAsync( async (cname, context, cancellationToken) =>
                {
                    var courseTable = await dbContext.Courses.ToListAsync(cancellationToken);
                    var foundCourse = courseTable.Where(c => c.CourseName == cname)
                                                 .FirstOrDefault();

                    if (foundCourse != null)
                    {
                        context.AddFailure(new ValidationFailure(
                                            "CourseName",
                                            "CourseName should be unique!"
                                           )
                        );
                    }
                });
        }

    }
}

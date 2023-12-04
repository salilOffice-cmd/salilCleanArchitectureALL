using Application.CoursesCQRS.Commands;
using Application.CoursesCQRS.Queries;
using Application.DTOs.CourseDTOs;
using Application.StudentsCQRS.Queries;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CourseController : APIControllerBase
    {
        private readonly IValidator<AddCourseDTO> addCourseValidator;
        public CourseController(IValidator<AddCourseDTO> _addCourseValidator)
        { 
            addCourseValidator = _addCourseValidator;
        }

        private ActionResult HandleValidationResult(ValidationResult validationResult)
        {
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { errors });
            }
            return null; // Return null if validation is successful
        }


        [HttpGet]
        [Route("courses/{courseID:min(1)}")]
        public async Task<ActionResult> GetCourseByIDAsync(int courseID)
        {
            var gotCourse = await Mediator.Send(new GetCourseByIDQuery { CourseID = courseID });
            return Ok(gotCourse);
        }


        [HttpGet]
        [Route("courses")]
        public async Task<ActionResult> GetAllCoursesAsync([FromQuery] int limit)
        {
            var gotAllCourses = await Mediator.Send(new GetAllCoursesQuery { limitCount = limit});
            return Ok(gotAllCourses);
        }



        [HttpPost]
        [Route("courses")]
        public async Task<ActionResult> AddCourseAsync([FromBody] AddCourseDTO _addCourseDTO)
        {
            //var validationResult = addCourseValidator.Validate(_addCourseDTO);
            var validationResult = await addCourseValidator.ValidateAsync(_addCourseDTO);

            // This repeated code is added to a function above 'HandleValidationResult'
            //if (!validationResult.IsValid)
            //{
            //    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            //    return BadRequest(new { errors });
            //}

            var result = HandleValidationResult(validationResult);
            if (result != null) return result;  

            var addCourseCommand = new AddCourseCommand { addCourseDTO = _addCourseDTO};
            var gotAddedCourse = await Mediator.Send(addCourseCommand);
            return Ok(gotAddedCourse);
        }


        

        [HttpDelete]
        [Route("courses/{courseID}")]
        public async Task<ActionResult> DeleteCourseByIDAsync(int courseID)
        {
            var gotMessage = await Mediator.Send(new DeleteCourseCommand { CourseID = courseID });
            return Ok(gotMessage);
        }


        [HttpPut]
        [Route("courses/{courseID}")]
        public async Task<ActionResult> UpdateCourse(int courseID, [FromBody] UpdateCourseDTO _updateCourseDTO)
        {
            var gotMessage = await Mediator.Send(new UpdateCourseCommand {
                                            courseId = courseID,
                                            UpdateCourseDTO = _updateCourseDTO
                                            });
            return Ok(gotMessage);
        }
    }
}

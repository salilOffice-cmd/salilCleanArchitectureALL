using Application.CQRS.CoursesCQRS.Commands;
using Application.CQRS.CoursesCQRS.Queries;
using Application.CQRS.CourseStudentCQRS.Queries;
using Application.DTOs.CourseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CourseController : APIControllerBase
    {
        [HttpPost]
        [Route("courses")]
        public async Task<ActionResult> AddCourseAsync([FromBody] AddCourseDTO _addCourseDTO)
        {
            var addCourseCommand = new AddCourseCommand { addCourseDTO = _addCourseDTO};
            var gotAddedCourse = await Mediator.Send(addCourseCommand);
            return Ok(gotAddedCourse);
        }

        [HttpGet]
        [Route("courses")]
        public async Task<ActionResult> GetAllCoursesAsync()
        {
            var gotAllCourses = await Mediator.Send(new GetAllCoursesQuery { });
            return Ok(gotAllCourses);
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


        [HttpGet]
        [Route("all-course-students")]
        public async Task<ActionResult> GetAllCourseStudentsAsync()
        {
            var gotList = await Mediator.Send(new GetAllCourseStudent { });
            return Ok(gotList);
        }


    }
}

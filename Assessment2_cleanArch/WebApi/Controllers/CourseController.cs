using Application.CoursesCQRS.Commands;
using Application.CoursesCQRS.Queries;
using Application.DTOs.CourseDTOs;
using Application.StudentsCQRS.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CourseController : APIControllerBase
    {
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddCourseAsync([FromBody] AddCourseDTO _addCourseDTO)
        {
            var addCourseCommand = new AddCourseCommand { addCourseDTO = _addCourseDTO};
            var gotAddedCourse = await Mediator.Send(addCourseCommand);
            return Ok(gotAddedCourse);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult> GetAllCoursesAsync()
        {
            var gotAllCourses = await Mediator.Send(new GetAllCoursesQuery { });
            return Ok(gotAllCourses);
        }

        [HttpDelete]
        [Route("delete/{courseID}")]
        public async Task<ActionResult> DeleteCourseByIDAsync(int courseID)
        {
            var gotMessage = await Mediator.Send(new DeleteCourseCommand { CourseID = courseID });
            return Ok(gotMessage);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateCourse([FromBody] UpdateCourseDTO _updateCourseDTO)
        {
            var gotMessage = await Mediator.Send(new UpdateCourseCommand { UpdateCourseDTO = _updateCourseDTO });
            return Ok(gotMessage);
        }
    }
}

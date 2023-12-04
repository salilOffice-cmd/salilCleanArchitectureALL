using Application.CQRS.CoursesCQRS.Queries;
using Application.CQRS.StudentsCQRS.Commands;
using Application.CQRS.StudentsCQRS.Queries;
using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using Application.StudentsCQRS.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class StudentController : APIControllerBase
    {
        [HttpPost]
        [Route("students")]
        public async Task<ActionResult> AddStudentAsync([FromBody] AddStudentDTO _addStudentDTO)
        {
            var addStudentCommand = new AddStudentCommand { addStudentDTO = _addStudentDTO };
            var gotAddedStudent = await Mediator.Send(addStudentCommand);
            return Ok(gotAddedStudent);
        }

        [HttpGet]
        [Route("students")]
        public async Task<ActionResult> GetAllStudentsAsync()
        {
            var gotAllStudents = await Mediator.Send(new GetAllStudentsQuery { });
            return Ok(gotAllStudents);
        }

        [HttpDelete]
        [Route("students/{studentID}")]
        public async Task<ActionResult> DeleteStudentByIDAsync(int studentID)
        {
            var gotMessage = await Mediator.Send(new DeleteStudentCommand { StudentID = studentID });
            return Ok(gotMessage);
        }

        [HttpPut]
        [Route("students/{studentID}")]
        public async Task<ActionResult> UpdateStudent(int studentID, [FromBody] UpdateStudentDTO _updateStudentDTO)
        {
            var gotMessage = await Mediator.Send(new UpdateStudentCommand {
                                                studentId = studentID,
                                                UpdateStudentDTO = _updateStudentDTO
                                            });
            return Ok(gotMessage);
        }


        [HttpGet]
        [Route("students/{studentID}/courses")]
        public async Task<ActionResult> GetCoursesByStudentIDAsync(int studentID)
        {
            var gotResponse = await Mediator.Send(new GetCoursesByStudentIDQuery { StudentID = studentID });
            return Ok(gotResponse);
        }


        [HttpPost]
        [Route("enrollStudentInCourse/{studentId}/{courseId}")]
        public async Task<ActionResult> EnrollStudentInCourseAsync( int studentId, int courseId)
        {
            var gotMessage = await Mediator.Send(
                            new EnrollStudentInCourseCommand { CourseID = courseId, StudentID = studentId });

            return Ok(new {gotMessage});
        }





    }
}

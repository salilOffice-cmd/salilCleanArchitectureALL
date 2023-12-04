using Application.CoursesCQRS.Commands;
using Application.DTOs.CourseDTOs;
using Application.DTOs.StudentDTOs;
using Application.StudentsCQRS.Commands;
using Application.StudentsCQRS.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class StudentController : APIControllerBase
    {
        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> AddStudentAsync([FromBody] AddStudentDTO _addStudentDTO)
        {
            var addStudentCommand = new AddStudentCommand { addStudentDTO = _addStudentDTO };
            var gotAddedStudent = await Mediator.Send(addStudentCommand);
            return Ok(gotAddedStudent);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<ActionResult> GetAllStudentsAsync()
        {
            var gotAllStudents = await Mediator.Send(new GetAllStudentsQuery { });
            return Ok(gotAllStudents);
        }

        [HttpDelete]
        [Route("delete/{studentID}")]
        public async Task<ActionResult> DeleteStudentByIDAsync(int studentID)
        {
            var gotMessage = await Mediator.Send(new DeleteStudentCommand { StudentID = studentID });
            return Ok(gotMessage);
        }

        [HttpPut]
        [Route("update")]
        public async Task<ActionResult> UpdateStudent([FromBody] UpdateStudentDTO _updateStudentDTO)
        {
            var gotMessage = await Mediator.Send(new UpdateStudentCommand { UpdateStudentDTO = _updateStudentDTO });
            return Ok(gotMessage);
        }
    }
}

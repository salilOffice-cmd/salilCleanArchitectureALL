using Application.DTOs.CourseStudentDTOs;
using AutoMapper;
using Domain.Entities;

namespace WebApi.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Student, Course, ViewCourseStudentDTO>()
                


        }
    }
}

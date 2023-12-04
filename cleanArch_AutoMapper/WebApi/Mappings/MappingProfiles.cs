using Application;
using Application.DTOs.StudentDTOs;
using AutoMapper;
using Domain.Entities;

namespace WebApi.Mappings
{

    // In ASP.NET Core, AutoMapper is a widely used library that
    // simplifies the mapping between different types.
    // It's particularly helpful when you need to transform objects from one type to another,
    // often used in scenarios like mapping domain entities to DTOs or vice versa.

    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {

            // 1. Getting Student

            // Mapping source --> Destination
            // CreateMap<Student, ViewStudentDTO>();


            // In case the name of the property are different in source and destination
            CreateMap<Student, ViewStudentDTO>()
                .ForMember(dest => dest.StudentName111,
                           opt => opt.MapFrom(src => src.StudentName));
            //// In case you have some extra logic when showing the property
            //.ForMember(dest => dest.CreatedDetails,
            //                   opt => opt.MapFrom(src => $"{src.CreatedBy} - {src.CreatedDate}"));




            // 2. Adding Student
            // source to destination
            //CreateMap<AddStudentDTO, Student>();

            CreateMap<AddStudentDTO, Student>()
                .ForMember(dest => dest.FullName,
                                   opt => opt.MapFrom(src => $"{src.StudentFirstName} {src.StudentLastName}"));



            // 3. Adding student
            // Condition: name of the student will be salil_sql 
            // studentName_courseName
            // courseName will be the name of the course corresponding
            // to the courseID the will be given by the student
            //CreateMap<AddStudentDTO, Student>()
            //    .ForMember(dest => dest.StudentName, 
            //               opt => opt.MapFrom<AddStudentResolver>()
            //    );



            // 4. Adding student
            // Condition: after the studentName is mapped, set the fullname to courseID + studentName
            CreateMap<AddStudentDTO, Student>()
                .ForMember(dest => dest.StudentName,
                           opt => opt.MapFrom<AddStudentResolver>()
                )
                .AfterMap((src, dest) =>
                {
                    dest.FullName = $"{src.CourseID} {dest.StudentName}";

                });


        }
    }



    class AddStudentResolver : IValueResolver<AddStudentDTO, Student, string>
    {
        private readonly IApplicationDBContext dbcontext;
        public AddStudentResolver(IApplicationDBContext _dbcontext)
        {
            dbcontext = _dbcontext;
        }

        public string Resolve(AddStudentDTO source, Student destination, string destMember, ResolutionContext context)
        {
            var courseTable = dbcontext.Courses.ToList();
            var gotCourse = courseTable.FirstOrDefault(c => c.CourseId == source.CourseID);

            
            var resultedString = $"{source.StudentName}_{gotCourse.CourseName}";
            return resultedString ;

        }
    }
}

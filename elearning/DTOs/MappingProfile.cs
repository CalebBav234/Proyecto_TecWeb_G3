using AutoMapper;
using DTOs;
using DTOS.user;

namespace Application;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
       
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.Profile));
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User")); 


        CreateMap<DTOs.Profile, ProfileDto>().ReverseMap();
        CreateMap<RegisterUserDto, DTOs.Profile>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher))
            .ForMember(dest => dest.Lessons, opt => opt.MapFrom(src => src.Lessons))
            .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments));
        CreateMap<CreateCourseDto, Course>();
        CreateMap<UpdateCourseDto, Course>();

        CreateMap<Lesson, LessonDto>().ReverseMap();
        CreateMap<CreateLessonDto, Lesson>();
        CreateMap<UpdateLessonDto, Lesson>();

        CreateMap<Enrollment, EnrollmentDto>().ReverseMap();
        CreateMap<CreateEnrollmentDto, Enrollment>();
        CreateMap<UpdateEnrollmentDto, Enrollment>();
    }
}

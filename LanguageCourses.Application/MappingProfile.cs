using AutoMapper;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageJobTitles.Domain.DataTransferObjects;

namespace LanguageCourses.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Student, StudentDto>()
            .ForMember(s => s.FullName, opt => opt
                .MapFrom(s => string.Join(' ', s.Surname, s.Name, s.MidName)
            )
        );
        CreateMap<StudentForCreationDto, Student>();
        CreateMap<StudentForUpdateDto, Student>();

        CreateMap<Course, CourseDto>();
        CreateMap<CourseForCreationDto, Course>();
        CreateMap<CourseForUpdateDto, Course>();

        CreateMap<Payment, PaymentDto>();
        CreateMap<PaymentForCreationDto, Payment>();
        CreateMap<PaymentForUpdateDto, Payment>();

        CreateMap<JobTitle, JobTitleDto>();
        CreateMap<JobTitleForCreationDto, JobTitle>();
        CreateMap<JobTitleForUpdateDto, JobTitle>();

        CreateMap<Employee, EmployeeDto>().ForMember(s => s.FullName, opt => opt
                .MapFrom(s => string.Join(' ', s.Surname, s.Name, s.Midname)
            )
        );
        CreateMap<EmployeeForCreationDto, Employee>();
        CreateMap<EmployeeForUpdateDto, Employee>();
    }
}

using AutoMapper;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;

namespace LanguageCourses.WebAPI;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Student, StudentDto>();
		CreateMap<Course, CourseDto>();
		CreateMap<Payment, PaymentDto>();
		CreateMap<JobTitle, JobTitleDto>();
		CreateMap<EmployeeDto, EmployeeDto>();
	}
}

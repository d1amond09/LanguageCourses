using AutoMapper;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Entities.DataTransferObjects;

namespace LanguageCourses.WebAPI;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Student, StudentDto>();
		CreateMap<Course, CourseDto>();
		CreateMap<Payment, PaymentDto>();
		CreateMap<JobTitle, JobTitleDto>();
		CreateMap<Employee, EmployeeDto>();
	}
}

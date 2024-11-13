using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;

namespace LanguageCourses.Application;

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

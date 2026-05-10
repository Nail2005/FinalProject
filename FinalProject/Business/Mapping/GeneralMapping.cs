using AutoMapper;
using Business.DTOs.AttendanceDto;
using Business.DTOs.AuditLogDto;
using Business.DTOs.CourseDto;
using Business.DTOs.GuidanceDto;
using Business.DTOs.LessonDto;
using Business.DTOs.NotificationDto;
using Business.DTOs.ParentDto;
using Business.DTOs.PaymentDto;
using Business.DTOs.StudentDto;
using Business.DTOs.TeacherDto;
using Business.DTOs.TeacherSalaryDto;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Student, CreateStudentDto>().ReverseMap();
            CreateMap<Student, UpdateStudentDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();

            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            
            CreateMap<Guidance, GuidanceDto>().ReverseMap();        
            CreateMap<Guidance, CreateGuidanceDto>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            CreateMap<Course, UpdateCourseDto>().ReverseMap();

            CreateMap<AuditLog, AuditLogDto>().ReverseMap();

            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Lesson, CreateLessonDto>().ReverseMap();

            CreateMap<Notification, NotificationDto>().ReverseMap();

            CreateMap<Payment, PaymentDto>() 
                .ForMember(dest=>dest.PaymentDate, 
                    opt=>opt.MapFrom(src=>src.PaidDate));
            CreateMap<Payment, CreatePaymentDto>().ReverseMap();

            CreateMap<Teacher, TeacherDto>()
                         .ForMember(dest => dest.FullName,
                            opt => opt.MapFrom(src => src.User.FullName))
                        .ForMember(dest => dest.Salary,
                           opt => opt.MapFrom(src => src.HourlySalary));
            CreateMap<Teacher, CreateTeacherDto>().ReverseMap();
            CreateMap<Teacher, UpdateTeacherDto>().ReverseMap();

            CreateMap<TeacherSalary, TeacherSalaryDto>().ReverseMap();

            CreateMap<Lesson, LessonDto>().ReverseMap();
            CreateMap<Lesson, CreateLessonDto>().ReverseMap();
            CreateMap<Lesson, UpdateLessonDto>().ReverseMap();

            CreateMap<Parent, ParentDto>().ReverseMap();
            CreateMap<Parent, UpdateParentDto>().ReverseMap();
        }

    }
}

using System;
using AutoMapper;
using Core.DTOs.Deserializers;
using Core.DTOs.Serializers;
using Core.Helpers;
using Core.Models;

namespace Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Serialize mapping
            CreateMap<Lesson, LessonDTO>();
            CreateMap<Student, StudentDTO>();
            CreateMap<Enrolment, EnrolmentDTO>();
            CreateMap<AttendanceSheet, AttendanceSheetDTO>()
                .ForMember(
                    dest => dest.Duration,
                    opt => opt.MapFrom(src => string.Format("{0:N2}", src.Duration.TotalHours))
                );
            CreateMap<Attendance, AttendanceForAttendanceSheetDTO>()
                .ForMember(
                    dest => dest.AttendanceType,
                    opt => opt.MapFrom(src => (AttendanceType)src.AttendanceType)
                );
            CreateMap<Attendance, AttendanceDTO>()
                .ForMember(
                    dest => dest.AttendanceType,
                    opt => opt.MapFrom(src => (AttendanceType)src.AttendanceType)
                );

            // Deserialize mapping
            CreateMap<PostStudentDTO, Student>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                );
            CreateMap<PutStudentDTO, Student>();

            CreateMap<PostLessonDTO, Lesson>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                );
            CreateMap<PutLessonDTO, Lesson>();

            CreateMap<PostAttendanceSheetDTO, AttendanceSheet>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                );
            CreateMap<PutAttendanceSheetDTO, AttendanceSheet>();

            CreateMap<PagedResult<AttendanceSheet>, PagedAttendanceSheetDTO>();
            CreateMap<PagedResult<Lesson>, PagedLessonDTO>();
            CreateMap<PagedResult<Student>, PagedStudentDTO>();
        }
    }
}
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

            CreateMap(typeof(PagedResult<>), typeof(PagedResultDTO<>));
        }
    }
}
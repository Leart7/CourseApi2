using AutoMapper;
using CourseApi.DTOs;
using CourseApi.Models;

namespace CourseApi.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<Course, CreateCourseRequestDto>()
                .ReverseMap();

            CreateMap<Course, UpdateCourseRequestDto>()
                .ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ReverseMap();

            CreateMap<Category, CreateCategoryRequestDto>()
                .ReverseMap();

            CreateMap<Category, UpdateCategoryRequestDto>()
                .ReverseMap();
        }
    }
}

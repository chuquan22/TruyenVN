using AutoMapper;
using TruyenVNAPI.Model;

namespace TruyenVNAPI.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Story, StoryDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Chapter, ChapterDTO>().ReverseMap();
            CreateMap<Viewed, ViewedDTO>().ReverseMap();
            CreateMap<Report, ReportDTO>().ReverseMap();
        }

    }
}

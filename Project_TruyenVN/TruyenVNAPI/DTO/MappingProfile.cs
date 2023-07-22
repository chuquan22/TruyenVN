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
        }

    }
}

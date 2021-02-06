using AutoMapper;
using UserAPI.Domain.Entities;
using UserAPI.Domain.ViewModels;

namespace UserAPI.Services.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel,User>();
        }
    }
}

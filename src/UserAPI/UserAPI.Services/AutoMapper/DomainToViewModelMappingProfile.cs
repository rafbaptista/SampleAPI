using AutoMapper;
using UserAPI.Domain.Entities;
using UserAPI.Domain.ViewModels;

namespace UserAPI.Services.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.JobName, opts => opts.MapFrom(src => src.Job.Name))
                .ForMember(dest => dest.JobDescription, opts => opts.MapFrom(src => src.Job.Description));
        }
    }
}

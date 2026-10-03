using AutoMapper;
using GoPress.Mvc.Areas.Admin.Models;

namespace GoPress.Mvc.Mapping
{
    public class AdminMappingProfile:Profile
    {
        public AdminMappingProfile()
        {
            CreateMap<
                AllApplicationUserProfileApiModel,
                AllApplicationUserProfileViewModel
            >();

            CreateMap<
                AllCustomerProfileApiModel,
                AllCutomerProfileViewModel
            >()
            .ForMember(
                dest => dest.AllApplicationUserProfileViewModel,
                opt => opt.MapFrom(
                    src => src.AllApplicationUserProfileDto
                )
            );
        }

    }
}

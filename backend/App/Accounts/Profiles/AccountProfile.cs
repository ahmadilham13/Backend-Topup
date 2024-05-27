using AutoMapper;
using backend.Accounts.Models.User;
using backend.BaseModule.Models.Media;
using backend.Entities;

namespace backend.Accounts.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AuthenticateResponse>()
            .ForMember(dest =>
                dest.Username,
                opt => opt.MapFrom( src => src.UserName ))
            .ForMember(dest =>
                dest.Email,
                opt => opt.MapFrom( src => src.Email ))
            .ForMember(dest =>
                dest.Menus,
                opt => opt.MapFrom( src => src.Role.Permission.Select(x => x.NavigationMenu).ToList() ))
            .AfterMap((src, dest) => 
            {
                dest.Avatar ??= new ImageUploadResponse()
                    {
                        ImageBig = "static-assets/img/account/profile-placeholder-bg.jpg",
                        ImageSmall = "static-assets/img/account/profile-placeholder-sm.jpg",
                        ImageDefault = "static-assets/img/account/profile-placeholder.jpg"
                    };
            });
    }
}
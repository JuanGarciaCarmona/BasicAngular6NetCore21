using AutoMapper;
using NetCore21.Authentication.Domain;
using NetCore21.Site.ViewModels;

namespace NetCore21.Site.Mappings
{
  public class RegistrationViewModelToAppUserProfile : Profile
  {
    public RegistrationViewModelToAppUserProfile()
    {
      CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
    }
  }
}

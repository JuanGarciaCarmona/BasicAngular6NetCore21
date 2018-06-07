using AutoMapper;
using NetCore21.Model.Entities;
using NetCore21.ViewModels;

namespace NetCore21.Mappings
{
  public class RegistrationViewModelToAppUserProfile : Profile
  {
    public RegistrationViewModelToAppUserProfile()
    {
      CreateMap<RegistrationViewModel, AppUser>().ForMember(au => au.UserName, map => map.MapFrom(vm => vm.Email));
    }
  }
}

using FluentValidation.Attributes;
using NetCore21.Site.ViewModels.Validations;

namespace NetCore21.Site.ViewModels
{
  [Validator(typeof(CredentialsViewModelValidator))]
  public class CredentialsViewModel
  {   
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}

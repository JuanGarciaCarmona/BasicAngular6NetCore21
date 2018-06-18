using FluentValidation.Attributes;
using NetCore21.ViewModels.Validations;

namespace NetCore21.ViewModels
{
  [Validator(typeof(CredentialsViewModelValidator))]
  public class CredentialsViewModel
  {   
    public string UserName { get; set; }
    public string Password { get; set; }
  }
}

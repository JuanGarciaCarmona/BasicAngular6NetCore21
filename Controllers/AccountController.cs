using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NetCore21.Data;
using NetCore21.Helper;
using NetCore21.Model.Entities;
using NetCore21.ViewModels;
using System.Threading.Tasks;

namespace NetCore21.Controllers
{
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly NetCore21DbContext _appDbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManager, IMapper mapper, NetCore21DbContext appDbContext)
    {
      _userManager = userManager;
      _mapper = mapper;
      _appDbContext = appDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userIdentity = _mapper.Map<AppUser>(model);

      var result = await _userManager.CreateAsync(userIdentity, model.Password);

      if (!result.Succeeded) return new BadRequestObjectResult(ErrorHelper.AddErrorsToModelState(result, ModelState));

      await _appDbContext.Customers.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
      await _appDbContext.SaveChangesAsync();

      return new OkObjectResult("Account succesfully created!");
    }
  }
}

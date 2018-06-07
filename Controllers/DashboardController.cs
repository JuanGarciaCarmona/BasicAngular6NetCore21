using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore21.Data;
using NetCore21.Model.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCore21.Controllers
{
  [Authorize(Policy = "ApiUser")]
  [Route("api/[controller]/[action]")]
  public class DashboardController : Controller
  {
    private readonly ClaimsPrincipal _caller;
    private readonly NetCore21DbContext _appDbContext;

    public DashboardController(UserManager<AppUser> userManager, NetCore21DbContext appDbContext, IHttpContextAccessor httpContextAccessor)
    {
      _caller = httpContextAccessor.HttpContext.User;
      _appDbContext = appDbContext;
    }

    // GET api/dashboard/home
    [HttpGet]
    public async Task<IActionResult> Home()
    {
      // retrieve the user info
      //HttpContext.User
      var userId = _caller.Claims.Single(c => c.Type == "id");
      var customer = await _appDbContext.Customers.Include(c => c.Identity).SingleAsync(c => c.Identity.Id == userId.Value);

      return new OkObjectResult(new
      {
        Message = "This is secure API and user data!",
        customer.Identity.Name,
        customer.Identity.NickName,
        customer.Identity.PictureUrl,
        customer.Identity.FacebookId,
        customer.Location,
        customer.Locale,
        customer.Gender
      });
    }
  }
}

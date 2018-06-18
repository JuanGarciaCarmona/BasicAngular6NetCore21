using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCore21.Auth;
using NetCore21.Data;
using NetCore21.Helper;
using NetCore21.Model.Entities;
using NetCore21.Model.Social.Facebook;
using NetCore21.ViewModels;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCore21.Controllers
{
  [Route("api/[controller]/[action]")]
  public class FacebookController : Controller
  {
    private readonly NetCore21DbContext _appDbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly FacebookAuthSettings _fbAuthSettings;
    private readonly IJwtFactory _jwtFactory;
    private readonly JwtOptions _jwtOptions;
    private static readonly HttpClient Client = new HttpClient();

    public FacebookController(IOptions<FacebookAuthSettings> fbAuthSettingsAccessor, UserManager<AppUser> userManager, NetCore21DbContext appDbContext, IJwtFactory jwtFactory, IOptions<JwtOptions> jwtOptions)
    {
      _fbAuthSettings = fbAuthSettingsAccessor.Value;
      _userManager = userManager;
      _appDbContext = appDbContext;
      _jwtFactory = jwtFactory;
      _jwtOptions = jwtOptions.Value;
    }

    // POST api/facebook/authenticate
    [HttpPost]
    public async Task<IActionResult> Authenticate([FromBody]FacebookAuthViewModel model)
    {
      // Generate an app access token
      var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthSettings.AppId}&client_secret={_fbAuthSettings.AppSecret}&grant_type=client_credentials");
      var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);

      // Validate the user access token
      var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
      var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

      if (!userAccessTokenValidation.Data.IsValid)
      {
        return BadRequest(ErrorHelper.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState));
      }

      // Request user data 
      var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={model.AccessToken}");
      var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

      // Upsert User
      var user = await _userManager.FindByEmailAsync(userInfo.Email);

      if (user == null)
      {
        var appUser = new AppUser
        {

          Name = userInfo.FirstName,
          FamilyName = userInfo.LastName,
          FacebookId = userInfo.Id,
          Email = userInfo.Email,
          UserName = userInfo.Email,
          PictureUrl = userInfo.Picture.Data.Url
        };

        var result = await _userManager.CreateAsync(appUser, Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

        if (!result.Succeeded) return new BadRequestObjectResult(ErrorHelper.AddErrorsToModelState(result, ModelState));

        await _appDbContext.Customers.AddAsync(new Customer { IdentityId = appUser.Id, Location = "", Locale = userInfo.Locale, Gender = userInfo.Gender });
        await _appDbContext.SaveChangesAsync();
      }

      // Generate JWT 
      var localUser = await _userManager.FindByNameAsync(userInfo.Email);

      if (localUser == null)
      {
        return BadRequest(ErrorHelper.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
      }

      var jwt = await TokenGenerator.GenerateJwt(
        _jwtFactory.GenerateClaimsIdentity(localUser.UserName, localUser.Id),
        _jwtFactory, localUser.UserName, _jwtOptions, new JsonSerializerSettings { Formatting = Formatting.Indented });

      return new OkObjectResult(jwt);
    }
  }
}

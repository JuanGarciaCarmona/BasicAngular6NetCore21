using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetCore21.Site.Auth
{
  public static class TokenGenerator
  {
    public static async Task<string> GenerateJwt(
      ClaimsIdentity identity,
      IJwtFactory jwtFactory,
      string userName,
      JwtOptions jwtOptions,
      JsonSerializerSettings serializerSettings)
    {
      var response = new
      {
        id = identity.Claims.Single(c => c.Type == "id").Value,
        authToken = await jwtFactory.GenerateEncodedToken(userName, identity),
        expiresIn = (int)jwtOptions.ValidFor.TotalSeconds
      };

      return JsonConvert.SerializeObject(response, serializerSettings);
    }
  }
}

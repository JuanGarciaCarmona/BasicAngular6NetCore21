using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NetCore21.Authentication.Abstract;
using NetCore21.Authentication.Domain;

namespace NetCore21.Authentication
{
  public class JwtFactory : IJwtFactory
  {
    private readonly JwtOptions _jwtOptions;

    public JwtFactory(IOptions<JwtOptions> jwtOptions)
    {
      if (jwtOptions == null)
      {
        throw new ArgumentNullException(nameof(jwtOptions));
      }
      else
      {
        _jwtOptions = jwtOptions.Value;
      }

      ThrowIfInvalidOptions(_jwtOptions);
    }

    public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
    {
      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, userName),
        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
        identity.FindFirst(Constants.JwtClaimIdentifiers.Rol),
        identity.FindFirst(Constants.JwtClaimIdentifiers.Id)
      };

      // Create the JWT security token and encode it.
      var jwt = new JwtSecurityToken(
          issuer: _jwtOptions.Issuer,
          audience: _jwtOptions.Audience,
          claims: claims,
          notBefore: _jwtOptions.NotBefore,
          expires: _jwtOptions.Expiration,
          signingCredentials: _jwtOptions.SigningCredentials);

      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      return encodedJwt;
    }

    public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
    {
      return new ClaimsIdentity(
        new GenericIdentity(userName, "Token"),
        new[]
        {
          new Claim(Constants.JwtClaimIdentifiers.Id, id),
          new Claim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess)
        });
    }

    /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
    private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() -
        new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    private static void ThrowIfInvalidOptions(JwtOptions options)
    {
      if (options == null) throw new ArgumentNullException(nameof(options));

      if (options.ValidFor <= TimeSpan.Zero)
      {
        throw new ArgumentOutOfRangeException(nameof(options), "ValidFor Must be a non-zero TimeSpan.");
      }

      if (options.SigningCredentials == null)
      {
        throw new ArgumentOutOfRangeException(nameof(options), "SigningCredentials must not be null.");
      }
    }
  }
}

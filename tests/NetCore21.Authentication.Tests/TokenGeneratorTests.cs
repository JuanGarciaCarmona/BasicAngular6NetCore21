using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NetCore21.Authentication.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetCore21.Authentication.Tests
{
  public class TokenGeneratorTests
  {
    [Fact]
    public async Task WhenGeneratingJwtTokenItShouldReturnAWellFormatedOne()
    {
      var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ASecretKeyGoesHere......"));

      var jwtOptions = new JwtOptions
      {
        Audience = "",
        Issuer = "",
        SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
        Subject = "ASubject",
        ValidFor = new TimeSpan(0, 0, 7200)
      };

      var jwtOptionsnMock = new Mock<IOptions<JwtOptions>>();
      jwtOptionsnMock.Setup(p => p.Value).Returns(jwtOptions);

      var jwtFactory = new JwtFactory(jwtOptionsnMock.Object);

      var identity = jwtFactory.GenerateClaimsIdentity("AUserName", "Id");


      var jwt = await TokenGenerator.GenerateJwt(
       identity,
       jwtFactory,
       "AUserName",
       jwtOptions,
       new JsonSerializerSettings { Formatting = Formatting.Indented });

      Assert.NotNull(jwt);
      dynamic jsonObject = JObject.Parse(jwt);

      string id = (string)jsonObject.id;
      string authToken = (string)jsonObject.authToken;
      string expiresIn = (string)jsonObject.expiresIn;

      Assert.Equal("Id", id);
      Assert.NotNull(authToken);
      Assert.Equal("7200", expiresIn);
    }

  }
}

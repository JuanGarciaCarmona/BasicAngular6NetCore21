using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NetCore21.Authentication.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NetCore21.Authentication.Tests
{
  public class JwtFactoryTests
  {
    private JwtFactory _objectToTest;
    private Mock<IOptions<JwtOptions>> _jwtOptionsnMock;
    private SymmetricSecurityKey _signingKey;

    public JwtFactoryTests()
    {
      _jwtOptionsnMock = new Mock<IOptions<JwtOptions>>();
      _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("ASecretKeyGoesHere......"));
    }

    [Fact]
    public void ConstructorTest()
    {
      Assert.Throws<ArgumentNullException>(() => new JwtFactory(null));

      _jwtOptionsnMock = new Mock<IOptions<JwtOptions>>();
      Assert.Throws<ArgumentNullException>(() => new JwtFactory(_jwtOptionsnMock.Object));

      // VALID FOR CHECK
      _jwtOptionsnMock.Setup(p => p.Value)
                .Returns(new JwtOptions { Audience = "", Issuer = "", SigningCredentials = null, Subject = "", ValidFor = TimeSpan.Zero });
      Assert.Throws<ArgumentOutOfRangeException>(() => new JwtFactory(_jwtOptionsnMock.Object));

      // SIGNING CREDENTIALS CHECK
      _jwtOptionsnMock.Setup(p => p.Value)
         .Returns(new JwtOptions { Audience = "", Issuer = "", SigningCredentials = null, Subject = "ASubject", ValidFor = new TimeSpan(0, 0, 7200) });
      Assert.Throws<ArgumentOutOfRangeException>(() => new JwtFactory(_jwtOptionsnMock.Object));

      // PROPER OPTIONS:
       _jwtOptionsnMock.Setup(p => p.Value)
        .Returns(new JwtOptions { Audience = "", Issuer = "", SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256), Subject = "ASubject", ValidFor = new TimeSpan(0, 0, 7200) });
      _objectToTest = new JwtFactory(_jwtOptionsnMock.Object);
      Assert.NotNull(_objectToTest);
    }

    [Fact]
    public void WhenGeneratingClaimsIdentityItShouldReturnAnIdentity()
    {
      _jwtOptionsnMock.Setup(p => p.Value)
       .Returns(new JwtOptions { Audience = "", Issuer = "", SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256), Subject = "ASubject", ValidFor = new TimeSpan(0, 0, 7200) });

      _objectToTest = new JwtFactory(_jwtOptionsnMock.Object);
      var identity = _objectToTest.GenerateClaimsIdentity("AUserName", "Id");
      Assert.NotNull(identity);
    }


    [Fact]
    public async Task WhenGeneratingEncodedTokenItShouldReturnAnAuthToken()
    {
      _jwtOptionsnMock.Setup(p => p.Value)
       .Returns(new JwtOptions { Audience = "", Issuer = "", SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256), Subject = "ASubject", ValidFor = new TimeSpan(0, 0, 7200) });

      _objectToTest = new JwtFactory(_jwtOptionsnMock.Object);
      var claims = _objectToTest.GenerateClaimsIdentity("UserName", "Id");

      var token = await _objectToTest.GenerateEncodedToken("UserName", claims);

      Assert.NotNull(token);

    }
  }
}

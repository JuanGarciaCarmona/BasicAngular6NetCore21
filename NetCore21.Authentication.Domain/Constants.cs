
namespace NetCore21.Authentication.Domain
{
  public static class Constants
  {
    // NOTE: this must be placed in a secrets storage
    private const string _secretKey = "NetCore21SuperSuperSuperSecretKeyGoesHere";

    public static string SecretKey { get => _secretKey; }

    public static class JwtClaimIdentifiers
    {
      public const string Rol = "rol", Id = "id";
    }

    public static class JwtClaims
    {
      public const string ApiAccess = "api_access";
    }
  }
}

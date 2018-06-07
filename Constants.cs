namespace NetCore21
{
  public static class Constants
  {
    // NOTE: this must be placed in a secrets storage
    public static string SecretKey = "NetCore21SecretKeyGoesHere";

    public static class Strings
    {
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
}

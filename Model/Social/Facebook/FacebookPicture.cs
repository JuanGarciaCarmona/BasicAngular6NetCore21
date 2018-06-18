using Newtonsoft.Json;

namespace NetCore21.Model.Social.Facebook
{
  public class FacebookPicture
  {
    public int Height { get; set; }

    public int Width { get; set; }

    [JsonProperty("is_silhouette")]
    public bool IsSilhouette { get; set; }

    public string Url { get; set; }
  }
}

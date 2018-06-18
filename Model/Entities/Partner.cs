namespace NetCore21.Model.Entities
{
  public class Partner
  {
    public int Id { get; set; }
    public string IdentityId { get; set; }
    public string CompanyName { get; set; }
    public string Locale { get; set; }

    public AppUser OwnerIdentity { get; set; }
  }
}

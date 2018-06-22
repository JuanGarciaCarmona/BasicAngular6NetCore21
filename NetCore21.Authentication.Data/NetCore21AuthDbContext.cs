using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCore21.Authentication.Domain;

namespace NetCore21.Site.Data
{
  public class NetCore21AuthDbContext : IdentityDbContext<AppUser>
  {
    public NetCore21AuthDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Partner> Partners { get; set; }
  }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetCore21.Site.Model.Entities;

namespace NetCore21.Site.Data
{
  public class NetCore21DbContext : IdentityDbContext<AppUser>
  {
    public NetCore21DbContext(DbContextOptions options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Partner> Partners { get; set; }
  }
}

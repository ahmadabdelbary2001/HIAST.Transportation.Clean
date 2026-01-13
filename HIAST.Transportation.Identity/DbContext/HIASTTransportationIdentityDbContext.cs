using HIAST.Transportation.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HIAST.Transportation.Identity.DbContext;

public class HIASTTransportationIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public HIASTTransportationIdentityDbContext(DbContextOptions<HIASTTransportationIdentityDbContext> options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HIASTTransportationIdentityDbContext).Assembly);
    }
}
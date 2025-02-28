using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.api.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options) 
        {
        
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "9e1f2540-0e09-46ec-8b49-b2e3c758c691";
            var writerRoleId = "27db0aec-9c3a-4d57-a137-14dfc4fa1d15";

            var roles = new List<IdentityRole>
{
new IdentityRole
{
    Id = readerRoleId,
    ConcurrencyStamp = readerRoleId,
    Name = "Reader",
    NormalizedName = "Reader".ToUpper()
},
new IdentityRole
{
    Id = writerRoleId,
    ConcurrencyStamp = writerRoleId,
    Name = "Writer",
    NormalizedName = "Writer".ToUpper()
}
};

            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}

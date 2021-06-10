using Microsoft.EntityFrameworkCore;

namespace FakeUserApi.Models
{
    public class FakeUserContext : DbContext
    {
        public FakeUserContext(DbContextOptions<FakeUserContext> options) : base(options)
        { }
        public DbSet<FakeUser> FakeUsers { get; set; }
    }
}

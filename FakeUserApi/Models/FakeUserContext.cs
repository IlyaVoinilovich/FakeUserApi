using Microsoft.EntityFrameworkCore;

namespace FakeUserApi.Models
{
    /// <summary>
    /// FakeUserContext
    /// </summary>
    public class FakeUserContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public FakeUserContext(DbContextOptions<FakeUserContext> options) : base(options)
        { }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<FakeUser> FakeUsers { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

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
        public DbSet<FakeUser> FakeUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public FakeUserContext(DbContextOptions<FakeUserContext> options) : base(options)
        {
            if((Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists()==false)
            Database.EnsureCreated();   // создаем бд с новой схемой
        }
    }
}

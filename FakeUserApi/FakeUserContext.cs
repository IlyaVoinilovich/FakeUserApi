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
        public DbSet<FakeUser> FakeUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public FakeUserContext(DbContextOptions<FakeUserContext> options) : base(options)
        {
            if (Database.CanConnect() == false)//проверка существует ли бд 
            Database.EnsureCreated();   // создаем бд с новой схемой
        }
    }
}

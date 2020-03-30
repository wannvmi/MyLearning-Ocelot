using System.Reflection;
using Demo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        public DbSet<Store> Store { get; set; }


#if DEBUG
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name
                        && level == LogLevel.Information)
                    .AddConsole();
            });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLoggerFactory(MyLoggerFactory);
#endif

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // alternately this is built-in to EF Core 2.2
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

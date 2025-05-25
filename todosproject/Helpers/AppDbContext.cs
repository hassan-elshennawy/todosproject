using todosproject.Entities;
using Microsoft.EntityFrameworkCore;

namespace todosproject.Helpers
{

    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("postgresDb"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.user)
                .WithMany(u => u.todos)
                .HasForeignKey(t => t.UserId);
        }

        public DbSet<User> Users { get; set; }
 
        public DbSet<Todo> Todos { get; set; }
    }
}

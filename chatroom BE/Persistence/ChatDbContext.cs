
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class ChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserInformation> UserInformations { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<Message> Messages { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=CHATROOM SERVER;Username=postgres;Password=postgres");
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserInformation)
                .WithOne(ui => ui.User)
                .HasForeignKey<UserInformation>(ui => ui.UserId);
        }
    }
}

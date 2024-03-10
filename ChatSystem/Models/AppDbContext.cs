using Microsoft.EntityFrameworkCore;

namespace ChatSystem.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Add DbSet properties for each entity you want to include in the database
        public DbSet<Member> Members { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<InvitationStatus> InvitationStatus { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MemberConnection> MemberConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add any model configuration here if needed
            // This method is called when the model for the context has been initialized,
            // but before the model has been locked down and used to initialize the context.
        }
    }
}

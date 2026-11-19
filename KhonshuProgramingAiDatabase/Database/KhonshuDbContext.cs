using KhonshuProgramingAiDatabase.Tables;
using Microsoft.EntityFrameworkCore;
namespace KhonshuProgramingAiDatabase.Database;

public sealed class KhonshuDbContext : DbContext
{
    public KhonshuDbContext(DbContextOptions<KhonshuDbContext> options) : base(options) {}

    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Person>(person => {
            person.HasKey(p => p.Id);
            // person.Property(p => p.Id).ValueGeneratedOnAdd();
            person.HasIndex(p => p.Email).IsUnique();
            person.HasIndex(p => p.PersonName).IsUnique();
            
            person.Property(p => p.Email).IsRequired().HasMaxLength(500);
            person.Property(p => p.PersonName).IsRequired().HasMaxLength(500);
            
            person.HasMany(p => p.Chats).WithOne(p => p.Person).HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Chat>(chat => {
            chat.HasKey(c => c.Id);
            chat.HasMany(c => c.Messages).WithOne(c => c.Chat).HasForeignKey(c => c.ChatId).OnDelete(DeleteBehavior.Cascade);
            chat.Property(c => c.Title).IsRequired();
        });

        modelBuilder.Entity<Message>(message => {
            message.HasKey(m => m.Id);
            message.Property(m => m.Text).IsRequired();
            message.Property(m => m.Role).HasConversion<String>().HasMaxLength(50);
        });
    }
}

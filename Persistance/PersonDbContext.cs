using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureIds(modelBuilder);
        ConfigureEntityRelationships(modelBuilder);
        ConfigureEnumConversions(modelBuilder);
    }

    public DbSet<Person> People { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<PersonRelationShip> PersonRelationShips { get; set; }

    private static void ConfigureIds(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Person>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Phone>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<PersonRelationShip>()
            .HasKey(c => c.Id);
    }

    private static void ConfigureEnumConversions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.Sex)
            .HasConversion<int>();
        modelBuilder.Entity<Phone>()
            .Property(p => p.PhoneType)
            .HasConversion<int>();
        modelBuilder.Entity<PersonRelationShip>()
            .Property(p => p.RelationType)
            .HasConversion<int>();
    }

    private static void ConfigureEntityRelationships(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .HasOne(p => p.City)
            .WithMany()
            .HasForeignKey(p => p.CityId)
            .IsRequired();

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Phone)
            .WithMany()
            .HasForeignKey(p => p.PhoneId)
            .IsRequired(false);
    }
}
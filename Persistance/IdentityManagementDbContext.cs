using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class IdentityManagementDbContext : DbContext
{
    public IdentityManagementDbContext(DbContextOptions<IdentityManagementDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureIds(modelBuilder);
        ConfigureEntityRelationships(modelBuilder);
        ConfigureEnumConversions(modelBuilder);
      //  SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var city1 = new City() { Id = 1, Name = "Tbilisi" };
        var city2 = new City() { Id = 2, Name = "Rustavi" };
        modelBuilder.Entity<City>().HasData(city1, city2);

        var phone1 = new Phone() { Id = 1, PhoneNumber = "59815657", PhoneType = PhoneType.Mobile };
        var phone2 = new Phone() { Id = 2, PhoneNumber = "59813658", PhoneType = PhoneType.Mobile };

        var person1 = new Person()
        {
            Id = 1,
            DateOfBirth = DateTime.Today.AddYears(-21),
            PrivateId = "12345678901",
            CityId = city1.Id,
            PhoneId = phone1.Id,
            FirstName = "Anri", LastName = "Kezeroti",
            Sex = Sex.Male
        };
        var person2 = new Person()
        {
            Id = 2,
            DateOfBirth = DateTime.Today.AddYears(-21),
            PrivateId = "12345678902",
            CityId = city2.Id,
            PhoneId = phone2.Id,
            FirstName = "David", LastName = "Kezeroti",
            Sex = Sex.Male,
        };

        modelBuilder.Entity<Person>().HasData(person1, person2);

        var personRelationShip = new PersonRelationShip()
        {
            Id = 1,
            RelationType = PersonRelationType.Acquaintance,
            RelatedFromPersonId = person1.Id,
            RelatedToPersonId = person2.Id
        };

        modelBuilder.Entity<PersonRelationShip>().HasData(personRelationShip);
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
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        ;

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Phone)
            .WithMany()
            .HasForeignKey(p => p.PhoneId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        ;
    }
}
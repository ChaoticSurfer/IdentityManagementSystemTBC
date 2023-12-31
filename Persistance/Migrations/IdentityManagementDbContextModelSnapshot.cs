﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistance;

#nullable disable

namespace Persistance.Migrations
{
    [DbContext(typeof(IdentityManagementDbContext))]
    partial class IdentityManagementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.24");

            modelBuilder.Entity("Domain.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageRelativeAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PrivateId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Sex")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("PhoneId");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Domain.PersonRelationShip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelatedFromPersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelatedToPersonId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelationType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RelatedFromPersonId");

                    b.HasIndex("RelatedToPersonId");

                    b.ToTable("PersonRelationShips");
                });

            modelBuilder.Entity("Domain.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Domain.Person", b =>
                {
                    b.HasOne("Domain.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Phone", "Phone")
                        .WithMany()
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("City");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("Domain.PersonRelationShip", b =>
                {
                    b.HasOne("Domain.Person", "RelatedFromPerson")
                        .WithMany()
                        .HasForeignKey("RelatedFromPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Person", "RelatedToPerson")
                        .WithMany()
                        .HasForeignKey("RelatedToPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RelatedFromPerson");

                    b.Navigation("RelatedToPerson");
                });
#pragma warning restore 612, 618
        }
    }
}

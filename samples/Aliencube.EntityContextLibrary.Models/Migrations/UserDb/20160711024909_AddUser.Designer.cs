using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Aliencube.EntityContextLibrary.Models;

namespace Aliencube.EntityContextLibrary.Models.Migrations.UserDb
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20160711024909_AddUser")]
    partial class AddUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aliencube.EntityContextLibrary.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasAnnotation("MaxLength", 64);

                    b.HasKey("UserId");

                    b.ToTable("User");
                });
        }
    }
}

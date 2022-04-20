using System;
using System.Collections.Generic;
using BookStoreApp.API.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookStoreApp.API.Data
{
    public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
    {
        public BookStoreDbContext()
        {
        }

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //needed for Identity stuff

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_ToTable");
            });

            // seeding roles
            var seedRoles = new[] {
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    Id = "eb0e6c14-5924-40ba-a158-2e21a2f0ef3c",
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "feb3de40-39ed-4b3b-8d62-5ba0662b6479",
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(seedRoles);

            // seeding users
            var hasher = new PasswordHasher<ApiUser>();
            var seedUsers = new[] {
                new ApiUser
                {
                    Id = "0991be93-9fde-4701-aaae-7c201ba8a61b",
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "Admin",
                },
                new ApiUser
                {
                    Id = "0ad538cd-3d73-4e0d-a0b5-99429657e987",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User",
                }
            };
            foreach(var user in seedUsers) {
                user.PasswordHash = hasher.HashPassword(user, "P@ssw0rd1");
            }
            modelBuilder.Entity<ApiUser>().HasData(seedUsers);

            // fill roles to users (seeded only)
            var userRoles = new[] {
                new IdentityUserRole<string> {
                    RoleId = "eb0e6c14-5924-40ba-a158-2e21a2f0ef3c",    // Admin Role
                    UserId = "0991be93-9fde-4701-aaae-7c201ba8a61b"     // Admin User
                },
                new IdentityUserRole<string> {
                    RoleId = "feb3de40-39ed-4b3b-8d62-5ba0662b6479",    // User Role
                    UserId = "0ad538cd-3d73-4e0d-a0b5-99429657e987"     // Commom User
                }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);

            // set ignore migrations for database-first models
            modelBuilder.Entity<Book>().Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<Author>().Metadata.SetIsTableExcludedFromMigrations(true);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using Identity.Application.Handlers;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<EmailConfirmationToken> EmailConfirmationTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; } 
        public DbSet<EventRole> EventRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            base.OnModelCreating(builder);
            builder.Entity<UserRole>().HasKey(h => new {h.RoleID, h.UserID});
            builder.Entity<UserEvent>().HasKey(h => new {h.UserId,h.EventId});

            List<Role> roles = [
                new() { Id = new Guid("1D39A1C4-5663-4767-80A8-F56C869BE5C8"), Name = "Admin", Description = "Admin"},
                new() { Id = new Guid("1348A93D-B890-434F-9D55-829E5E8F3A8B"), Name = "Staff", Description = "Staff"},
                new() { Id = new Guid("80AE6925-A266-455C-9D0C-DC4CD3205BA4"), Name = "User", Description = "User"}
            ];

            builder.Entity<Role>()
                .HasData(roles);

            List<EventRole> eventRoles = [
                new() { Id = new Guid("C57B6454-603A-4597-8200-E09F8CA795FE"), Name = "Guest", Description = "Khách hàng bình thường" },
                new() { Id = new Guid("1348A93D-B890-434F-9D55-829E5E8F3A8B"), Name = "RegistrationLinkGuest", Description = "Khách hàng tham gia bằng link" },
                new() { Id = new Guid("9345487F-F275-46F0-8751-E3D35994E410"), Name = "PaidGuest", Description = "Khách hàng trả phí" },
                new() { Id = new Guid("7FD3C0A5-6883-4D0D-949A-1300D101E71B"), Name = "FreeGuest", Description = "Khách hàng miễn phí" },
                new() { Id = new Guid("80AE6925-6883-4D0D-949A-E3D35994E410"), Name = "Employee", Description = "Nhân viên" },
            ];

            builder.Entity<EventRole>()
               .HasData(eventRoles);

            List<User> users = [
                new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    PasswordHash = passwordHasher.HashPassword("aA@123"),
                    UserName = "admin",
                    IsEmailConfirmed = true,
                    Phone = "0386040060"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Staff",
                    LastName = "Staff",
                    Email = "staff@gmail.com",
                    PasswordHash = passwordHasher.HashPassword("aA@123"),
                    UserName = "staff",
                    IsEmailConfirmed = true,
                    Phone = "0372599558"
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "User",
                    LastName = "User",
                    Email = "user@gmail.com",
                    PasswordHash = passwordHasher.HashPassword("aA@123"),
                    UserName = "user",
                    IsEmailConfirmed = true,
                    Phone = "0372599559"
                }
            ];

            builder.Entity<User>()
                .HasData(users);

            List<User> eventUsers = [
                    new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Guest",
                        LastName = "1",
                        Email = "guest1@gmail.com",
                        PasswordHash = passwordHasher.HashPassword("aA@123"),
                        UserName = "guest1",
                        IsEmailConfirmed = true,
                        Phone = "0386040060",
                        Gender = 1,
                        IsPhoneConfirmed = true,
                        CreateById = users[0].Id
                    },
                    new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Guest",
                        LastName = "2",
                        Email = "guest2@gmail.com",
                        PasswordHash = passwordHasher.HashPassword("aA@123"),
                        UserName = "guest2",
                        IsEmailConfirmed = true,
                        Phone = "0372599558",
                        Gender = 1,
                        IsPhoneConfirmed = true,
                        CreateById = users[0].Id
                    },
                    new User()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Guest",
                        LastName = "3",
                        Email = "guest3@gmail.com",
                        PasswordHash = passwordHasher.HashPassword("aA@123"),
                        UserName = "guest3",
                        IsEmailConfirmed = true,
                        Phone = "0372599559",
                        Gender = 1,
                        IsPhoneConfirmed = true,
                        CreateById = users[0].Id
                    }
            ];

            builder.Entity<User>()
                .HasData(eventUsers);

            List<UserRole> userRoles = [
                new() { RoleID = roles[0].Id, UserID = users[0].Id },
                new() { RoleID = roles[1].Id, UserID = users[1].Id },
                new() { RoleID = roles[2].Id, UserID = users[2].Id },
                new() { RoleID = roles[2].Id, UserID = eventUsers[0].Id },
                new() { RoleID = roles[2].Id, UserID = eventUsers[1].Id },
                new() { RoleID = roles[2].Id, UserID = eventUsers[2].Id }
            ];

            builder.Entity<UserRole>()
                .HasData(userRoles);

            List<UserEvent> eventUserRoles = [
                new() { UserId = eventUsers[0].Id, EventId = Guid.NewGuid(), EventRoleId = eventRoles[0].Id },
                new() { UserId = eventUsers[1].Id, EventId = Guid.NewGuid(), EventRoleId = eventRoles[1].Id },
                new() { UserId = eventUsers[2].Id, EventId = Guid.NewGuid(), EventRoleId = eventRoles[2].Id }
            ];

            builder.Entity<UserEvent>()
                .HasData(eventUserRoles);
        }
    }
}

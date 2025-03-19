using Event.Domain.Entities;
using Event.Domain.Entities.Json;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Event.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Events> Events { get; set; }
        public DbSet<EventType>  EventTypes { get; set; }
        public DbSet<EventImages> EventImages { get; set; }
        public DbSet<EventLayout> EventLayout { get; set; }
        public DbSet<Layout> Layout { get; set; }
        public DbSet<EventSeats> EventSeats { get; set; }
        public DbSet<EventBooking> EventSeatsBookings { get; set; }
        public DbSet<EventTimeLine> EventTimeLine { get; set; }
        public DbSet<EventInvite> EventInvite { get; set; }
        public DbSet<EventDocuments> EventDocuments { get; set; }
        public DbSet<EventRegistrationLink> EventRegistrationLink { get; set; }
        public DbSet<EventPackages> EventPackages { get; set; }
        public DbSet<EventPackageRegistrations> EventPackageRegistrations { get; set; }
        public DbSet<EventPayment> EventPayments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EventTimeLine>()
                         .Property(e => e.HandleBy)
                         .HasConversion(
                             v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                             v => JsonSerializer.Deserialize<HandleBy?>(v, new JsonSerializerOptions())!
                         )
                         .HasColumnType("NVARCHAR(MAX)");



            builder.Entity<EventPackages>()
          .Property(e => e.Benefit)
          .HasConversion(
              v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
              v => JsonSerializer.Deserialize<Benefit>(v, new JsonSerializerOptions())!
          )
          .HasColumnType("NVARCHAR(MAX)");
        }
    }
}


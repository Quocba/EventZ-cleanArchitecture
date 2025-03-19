using EventProduct.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventProduct.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<Domain.Entities.EventProduct> EventProducts { get; set; }
        public DbSet<EventOrder> EventOrders { get; set; }
        public DbSet<EventOrderProduct> EventOrderProducts { get; set; }

        public DbSet<EventCategory> EventCategories { get; set; }
      
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}


using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<PaymentEvent> PaymentEvents { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SubcontractProfile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubcontractProfile.Entity.Model.Mapping
{
   public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>()
            .HasKey(p => new { p.prodId });
            modelBuilder.Entity<Lov_Config>()
           .HasKey(l => new { l.LOV_ID });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Lov_Config> Lov_Config { get; set; }
    }
}

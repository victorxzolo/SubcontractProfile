using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace SubcontractProfile.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => new { t.prodId, t.prodCode, t.prodName });

            // Properties
            this.Property(t => t.prodId)
                .IsRequired(); 
               

            this.Property(t => t.prodCode)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.prodName)
                .IsRequired()
                .HasMaxLength(30);

         //   this.Property(t => t.prodCost)
                
               
            this.ToTable("Product", "WebMvc");
            this.Property(t => t.prodId).HasColumnName("prodId");
            this.Property(t => t.prodCode).HasColumnName("prodCode");
            this.Property(t => t.prodName).HasColumnName("prodName");
           // this.Property(t => t.prodCost).HasColumnName("prodCost");

        }
    }
}
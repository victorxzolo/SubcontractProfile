using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Text;

namespace SubcontractProfile.Entity.Model.Mapping
{
     public class Lov_ConfigMap : EntityTypeConfiguration<Lov_Config>
    {
        public Lov_ConfigMap()
        {
            this.HasKey(t => new { t.LOV_ID });
            // Properties
            this.Property(t => t.LOV_ID)
                .IsRequired();


            this.Property(t => t.CREATED_BY);
              

            this.Property(t => t.CREATED_DATE);


            this.Property(t => t.UPDATED_BY);


            this.Property(t => t.UPDATED_DATE);

            this.Property(t => t.LOV_TYPE);


            this.Property(t => t.LOV_NAME);


            this.Property(t => t.DISPLAY_VAL);


            this.Property(t => t.LOV_VAL1);


            this.Property(t => t.LOV_VAL2);


            this.Property(t => t.LOV_VAL3);


            this.Property(t => t.LOV_VAL4);


            this.Property(t => t.LOV_VAL5);


            this.Property(t => t.ACTIVEFLAG);


            this.Property(t => t.ORDER_BY);
            
            
            //   this.Property(t => t.prodCost)


            this.ToTable("Lov_Config", "WebMvc");
            this.Property(t => t.LOV_ID).HasColumnName("LOV_ID");
            this.Property(t => t.CREATED_BY).HasColumnName("CREATED_BY");
            this.Property(t => t.CREATED_DATE).HasColumnName("CREATED_DATE");

     
            this.Property(t => t.UPDATED_BY).HasColumnName("UPDATED_BY");
            this.Property(t => t.UPDATED_DATE).HasColumnName("UPDATED_DATE");
            this.Property(t => t.LOV_TYPE).HasColumnName("LOV_TYPE");
            this.Property(t => t.LOV_NAME).HasColumnName("LOV_NAME");
            this.Property(t => t.DISPLAY_VAL).HasColumnName("DISPLAY_VAL");
            this.Property(t => t.LOV_VAL1).HasColumnName("LOV_VAL1");
            this.Property(t => t.LOV_VAL2).HasColumnName("LOV_VAL2");
            this.Property(t => t.LOV_VAL3).HasColumnName("LOV_VAL3");
            this.Property(t => t.LOV_VAL4).HasColumnName("LOV_VAL4");
            this.Property(t => t.LOV_VAL5).HasColumnName("LOV_VAL5");
            this.Property(t => t.ACTIVEFLAG).HasColumnName("ACTIVEFLAG");
            this.Property(t => t.ORDER_BY).HasColumnName("ORDER_BY");
        }
    }
}

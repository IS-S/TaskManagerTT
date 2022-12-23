using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain;

namespace TaskManager.DataAccessLayer.Configurations 
{
    public class ConfigEntityBase : IEntityTypeConfiguration<SiBaseEntity> // DB mapping configuration for Base POCO
    {
        public void Configure(EntityTypeBuilder<SiBaseEntity> builder)
        {
//            builder.Ignore(x => x.State);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd().IsRequired().HasField("_id").HasColumnName("Id").HasColumnType("int");
            
            builder.Property(x => x.Name)
                .IsRequired().HasField("_name").HasColumnName("Name").HasColumnType("varchar(128)");

            builder.Property(x => x.ObjVersion)
                .IsRowVersion()
                .HasColumnName("ObjVersion")
                .HasColumnType("rowversion");

        }
    }
}

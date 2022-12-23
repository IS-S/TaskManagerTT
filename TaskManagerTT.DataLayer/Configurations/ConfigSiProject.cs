using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.DataAccessLayer.Configurations
{
    // DB mapping configuration for Project instance class
    public class ConfigSiProject : IEntityTypeConfiguration<SiProject> 
    {
        public void Configure(EntityTypeBuilder<SiProject> builder)
        {
 
            builder.Property(x => x.Status)
                .HasColumnName("Status").HasColumnType("smallint");
            builder.Property(x => x.Priority)
                .HasColumnName("Priority").HasColumnType("int");
            builder.Property(x => x.StartDate)
                .HasColumnName("StartDate").HasColumnType("date");
            builder.Property(x => x.CompletionDate)
                .HasColumnName("CompletionDate").HasColumnType("date");
            builder.HasMany<SiTask>(x => x.Tasks)
                .WithOne(x => x.Project).HasForeignKey(x => x.ProjectId);

        }
    }
}

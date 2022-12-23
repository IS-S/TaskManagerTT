using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.DataAccessLayer.Configurations
{
    // DB mapping configuration for Task instance class
    public class ConfigSiTask : IEntityTypeConfiguration<SiTask>
    {
        public void Configure(EntityTypeBuilder<SiTask> builder)
        {

            builder.HasOne<SiProject>(x => x.Project)
                .WithMany(x => x.Tasks)
                .HasForeignKey(x => x.ProjectId);

            builder.Property(x => x.Description)
                .HasColumnName("Description").HasColumnType("varchar(MAX)");
            builder.Property(x => x.Priority)
                .HasColumnName("Priority").HasColumnType("int");
            builder.Property(x => x.Status)
                .HasColumnName("Status").HasColumnType("smallint");
        }
    }
}

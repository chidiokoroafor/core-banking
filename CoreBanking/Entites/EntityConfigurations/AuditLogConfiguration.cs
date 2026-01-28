using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Entites.EntityConfigurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> entity)
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");


            entity.Property(a => a.EntityName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.Action)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(a => a.PerformedBy)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.CreatedAt)
                .IsRequired();
        }
    }
}

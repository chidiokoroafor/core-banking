using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Entites.EntityConfigurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer> entity)
        {
            entity.HasKey(t => t.Id);

            entity.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            entity.Property(t => t.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.HasCheckConstraint(
                "CK_Transfers_Amount_Positive",
                "[Amount] > 0"
            );


            entity.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(20);

            //entity.Property(t => t.IdempotencyKey)
            //    .IsRequired()
            //    .HasMaxLength(100);

            entity.Property(t => t.Reference)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(t => t.Reference)
                .IsUnique();

            entity.Property(t => t.CreatedAt)
                .IsRequired();

            entity.HasOne(t => t.FromAccount)
                .WithMany()
                .HasForeignKey(t => t.FromAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.ToAccount)
                .WithMany()
                .HasForeignKey(t => t.ToAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

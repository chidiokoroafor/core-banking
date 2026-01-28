using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Entites.EntityConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> entity)
        {
            entity.HasKey(l => l.Id);

            entity.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");


            entity.Property(l => l.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.HasCheckConstraint(
                "CK_Transaction_Amount_Positive",
                "[Amount] <> 0"
            );

            entity.Property(l => l.TransactionType)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(l => l.CreatedAt)
                .IsRequired();

            entity.HasOne(l => l.Account)
                .WithMany(a => a.LedgerEntries)
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(l => l.Transfer)
                .WithMany()
                .HasForeignKey(l => l.TransferId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

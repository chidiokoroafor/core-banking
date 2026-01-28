using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Entites.EntityConfigurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> entity)
        {
            entity.HasKey(e => e.Id);

            entity.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            entity.Property(a => a.AccountNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasIndex(a => a.AccountNumber)
                .IsUnique();

            entity.Property(a => a.CustomerId)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(a => a.Balance)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(a => a.Status)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(a => a.CreatedAt)
                .IsRequired();

            entity.Property(a => a.RowVersion)
                .IsRowVersion();

            entity.HasOne(a => a.Customer)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(a => a.LedgerEntries)
                .WithOne(l => l.Account)
                .HasForeignKey(l => l.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

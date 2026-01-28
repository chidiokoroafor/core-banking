using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Entites.EntityConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> entity)
        {
            entity.HasKey(c => c.Id);
            entity.Property(a => a.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            entity.Property(c => c.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(c => c.Email).IsUnique();
            entity.Property(c => c.PasswordHash).IsRequired();
            entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(50);
            entity.Property(c => c.CreatedAt).IsRequired();

            entity.HasMany(c => c.Accounts)
                  .WithOne(a => a.Customer)
                  .HasForeignKey(a => a.CustomerId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(c => c.Accounts)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

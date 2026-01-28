using CoreBanking.Entites;
using CoreBanking.Entites.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ReferenceSequence> ReferenceSequence { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new TransferConfiguration());
            builder.ApplyConfiguration(new AuditLogConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new ReferenceSequenceConfiguration());
        }
    }
}

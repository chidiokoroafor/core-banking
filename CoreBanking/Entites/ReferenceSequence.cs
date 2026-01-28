using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreBanking.Entites
{
    public class ReferenceSequence
    {
        public DateTime Date { get; set; }
        public int LastNumber { get; set; }
    }

    public class ReferenceSequenceConfiguration : IEntityTypeConfiguration<ReferenceSequence>
    {
        public void Configure(EntityTypeBuilder<ReferenceSequence> builder)
        {
            builder.HasKey(r => r.Date);

            builder.Property(r => r.Date)
                   .HasColumnType("date");

            builder.Property(r => r.LastNumber)
                   .IsRequired();
        }
    }
}

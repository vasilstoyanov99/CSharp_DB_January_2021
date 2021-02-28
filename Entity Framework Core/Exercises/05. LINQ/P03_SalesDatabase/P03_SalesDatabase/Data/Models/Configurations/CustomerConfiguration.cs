using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P03_SalesDatabase.Data.Models.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.CustomerId);

            builder
                .Property(x => x.Name)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder
                .Property(x => x.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
        }
    }
}

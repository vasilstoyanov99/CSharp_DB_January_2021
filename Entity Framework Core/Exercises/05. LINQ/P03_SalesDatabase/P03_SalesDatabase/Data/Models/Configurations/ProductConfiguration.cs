using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P03_SalesDatabase.Data.Models.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.ProductId);

            builder
                .Property(x => x.Name)
                .HasMaxLength(50)
                .IsUnicode(true);

            builder
                .Property(x => x.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description")
                .IsUnicode(true);
        }
    }
}

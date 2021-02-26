using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.CourseId);

            builder
                .Property(x => x.Name)
                .HasMaxLength(80)
                .IsUnicode(true)
                .IsRequired(true);

            builder
                .Property(x => x.Description)
                .IsUnicode(true)
                .IsRequired(false);

            builder
                .Property(x => x.StartDate)
                .IsRequired(true);

            builder
                .Property(x => x.EndDate)
                .IsRequired(true);

            builder
                .Property(x => x.Price)
                .IsRequired(true);
        }
    }
}

using System.Security.Cryptography.X509Certificates;
using SoftJail.Data.Models;

namespace SoftJail.Data
{
	using Microsoft.EntityFrameworkCore;

	public class SoftJailDbContext : DbContext
	{
		public SoftJailDbContext()
		{
		}

		public SoftJailDbContext(DbContextOptions options)
			: base(options)
		{
		}

        public DbSet<Prisoner> Prisoners { get; set; }

        public DbSet<Officer> Officers { get; set; }

        public DbSet<Cell> Cells { get; set; }

        public DbSet<Mail> Mails { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<OfficerPrisoner> OfficersPrisoners { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder
					.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OfficerPrisoner>(entity =>
            {
                //entity.HasKey(k => new {k.OfficerId, k.PrisonerId});

                //entity
                //    .HasOne(x => x.Officer)
                //    .WithMany(x => x.OfficerPrisoners)
                //    .HasForeignKey(x => x.OfficerId)
                //    .OnDelete(DeleteBehavior.Restrict);

                //entity.HasOne(x => x.Prisoner)
                //    .WithMany(x => x.PrisonerOfficers)
                //    .HasForeignKey(x => x.PrisonerId)
                //    .OnDelete(DeleteBehavior.Restrict);

                entity.HasKey(op => new { op.PrisonerId, op.OfficerId });

                entity
                    .HasOne(op => op.Prisoner)
                    .WithMany(p => p.PrisonerOfficers)
                    .HasForeignKey(op => op.PrisonerId)
                    .OnDelete(DeleteBehavior.Restrict);

            });
        }
	}
}
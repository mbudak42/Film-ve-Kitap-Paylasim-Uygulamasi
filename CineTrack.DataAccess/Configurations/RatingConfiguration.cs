namespace CineTrack.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CineTrack.DataAccess.Entities;

public class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
	public void Configure(EntityTypeBuilder<Rating> builder)
	{
		builder.HasKey(r => new { r.UserId, r.ContentId });

		builder.Property(r => r.RatingValue).IsRequired();

		builder.HasOne(r => r.User)
			.WithMany(u => u.Ratings)
			.HasForeignKey(r => r.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(r => r.Content)
			.WithMany(c => c.Ratings)
			.HasForeignKey(r => r.ContentId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}

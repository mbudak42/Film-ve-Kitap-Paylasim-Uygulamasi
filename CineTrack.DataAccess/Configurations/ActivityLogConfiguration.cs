namespace CineTrack.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CineTrack.DataAccess.Entities;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
	public void Configure(EntityTypeBuilder<ActivityLog> builder)
	{
		builder.HasKey(a => a.Id);

		builder.HasOne(a => a.User)
			.WithMany(u => u.Activities)
			.HasForeignKey(a => a.UserId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(a => a.Content)
			.WithMany(c => c.Activities)
			.HasForeignKey(a => a.ContentId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}

namespace CineTrack.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CineTrack.DataAccess.Entities;

public class ListContentConfiguration : IEntityTypeConfiguration<ListContent>
{
	public void Configure(EntityTypeBuilder<ListContent> builder)
	{
		builder.HasKey(lc => new { lc.ListId, lc.ContentId });

		builder.HasOne(lc => lc.UserList)
			.WithMany(l => l.ListContents)
			.HasForeignKey(lc => lc.ListId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(lc => lc.Content)
			.WithMany(c => c.ListContents)
			.HasForeignKey(lc => lc.ContentId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}

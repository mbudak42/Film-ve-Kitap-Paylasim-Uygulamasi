namespace CineTrack.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CineTrack.DataAccess.Entities;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
	public void Configure(EntityTypeBuilder<Content> builder)
	{
		builder.HasKey(c => c.Id);

		builder.Property(c => c.Title).IsRequired();
		builder.Property(c => c.ContentType).IsRequired();
	}
}

namespace CineTrack.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CineTrack.DataAccess.Entities;

public class UserListConfiguration : IEntityTypeConfiguration<UserList>
{
	public void Configure(EntityTypeBuilder<UserList> builder)
	{
		builder.HasKey(l => l.Id);

		builder.Property(l => l.ListName).IsRequired();

		builder.HasOne(l => l.User)
			.WithMany(u => u.UserLists)
			.HasForeignKey(l => l.UserId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}

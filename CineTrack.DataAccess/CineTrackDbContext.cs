using Microsoft.EntityFrameworkCore;
using CineTrack.DataAccess.Entities;

namespace CineTrack.DataAccess;

public class CineTrackDbContext : DbContext
{
	public CineTrackDbContext(DbContextOptions<CineTrackDbContext> options)
		: base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Content> Contents { get; set; }
	public DbSet<Rating> Ratings { get; set; }
	public DbSet<Review> Reviews { get; set; }
	public DbSet<Follow> Follows { get; set; }
	public DbSet<UserList> UserLists { get; set; }
	public DbSet<ListContent> ListContents { get; set; }
	public DbSet<ActivityLog> ActivityLogs { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
{
	base.OnModelCreating(modelBuilder);

	modelBuilder.ApplyConfigurationsFromAssembly(typeof(CineTrackDbContext).Assembly);
}

}

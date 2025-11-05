using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CineTrack.DataAccess;

public class CineTrackDbContextFactory : IDesignTimeDbContextFactory<CineTrackDbContext>
{
	public CineTrackDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<CineTrackDbContext>();

		optionsBuilder.UseSqlServer(
			"Server=(localdb)\\MSSQLLocalDB;Database=CineTrackDB;Trusted_Connection=True;MultipleActiveResultSets=true;"
		);

		return new CineTrackDbContext(optionsBuilder.Options);
	}
}

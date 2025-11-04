namespace CineTrack.DataAccess.Entities;

public class Content
{
	public string Id { get; set; } // External API ID (TMDB/Books)
	public string Title { get; set; }
	public string ContentType { get; set; } // movie or book
	public string? CoverUrl { get; set; }
	public string? MetadataJson { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	// Navigation Properties
	public ICollection<Review>? Reviews { get; set; }
	public ICollection<Rating>? Ratings { get; set; }
	public ICollection<ListContent>? ListContents { get; set; }
	public ICollection<ActivityLog>? Activities { get; set; }
}

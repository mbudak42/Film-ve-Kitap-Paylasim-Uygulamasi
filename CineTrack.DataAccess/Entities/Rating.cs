namespace CineTrack.DataAccess.Entities;

public class Rating
{
	public int UserId { get; set; }
	public string ContentId { get; set; }
	public int RatingValue { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public User User { get; set; }
	public Content Content { get; set; }
}

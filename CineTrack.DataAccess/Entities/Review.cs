namespace CineTrack.DataAccess.Entities;

public class Review
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public string ContentId { get; set; }
	public string ReviewText { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public User User { get; set; }
	public Content Content { get; set; }
}

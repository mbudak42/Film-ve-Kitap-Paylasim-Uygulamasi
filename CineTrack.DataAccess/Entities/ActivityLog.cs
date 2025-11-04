namespace CineTrack.DataAccess.Entities;

public class ActivityLog
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public string? ContentId { get; set; }
	public string ActionType { get; set; }
	public string? ActionData { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public User User { get; set; }
	public Content? Content { get; set; }
}

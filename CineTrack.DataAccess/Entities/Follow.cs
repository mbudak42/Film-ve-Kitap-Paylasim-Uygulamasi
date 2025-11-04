namespace CineTrack.DataAccess.Entities;

public class Follow
{
	public int FollowerId { get; set; }
	public int FollowedId { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public User Follower { get; set; }
	public User Followed { get; set; }
}

namespace CineTrack.DataAccess.Entities;

public class UserList
{
	public int Id { get; set; }
	public int UserId { get; set; }
	public string ListName { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public User User { get; set; }
	public ICollection<ListContent>? ListContents { get; set; }
}

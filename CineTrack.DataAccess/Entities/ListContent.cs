namespace CineTrack.DataAccess.Entities;

public class ListContent
{
	public int ListId { get; set; }
	public string ContentId { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public UserList UserList { get; set; }
	public Content Content { get; set; }
}

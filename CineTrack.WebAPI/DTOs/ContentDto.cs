namespace CineTrack.WebAPI.DTOs;

public class ContentDto
{
	public string Id { get; set; }
	public string Title { get; set; }
	public string ContentType { get; set; } // movie | book
	public string? CoverUrl { get; set; }
	public string? MetadataJson { get; set; }
}

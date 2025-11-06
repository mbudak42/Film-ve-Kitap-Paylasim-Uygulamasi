using Microsoft.AspNetCore.Mvc;
using CineTrack.WebAPI.Services;

namespace CineTrack.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : ControllerBase
{
	private readonly ContentService _contentService;

	public ContentController(ContentService contentService)
	{
		_contentService = contentService;
	}

	// 1) Film arama
	[HttpGet("search/movies")]
	public async Task<IActionResult> SearchMovies([FromQuery] string q)
	{
		var results = await _contentService.SearchMoviesAsync(q);
		return Ok(results);
	}

	// 2) Kitap arama
	[HttpGet("search/books")]
	public async Task<IActionResult> SearchBooks([FromQuery] string q)
	{
		var results = await _contentService.SearchBooksAsync(q);
		return Ok(results);
	}

	// 3) Detay getir
	[HttpGet("{type}/{id}")]
	public async Task<IActionResult> GetContent(string type, string id)
	{
		var content = await _contentService.GetContentAsync(id, type);
		if (content == null)
			return NotFound(new { message = "Icerik bulunamadi." });

		return Ok(content);
	}
}

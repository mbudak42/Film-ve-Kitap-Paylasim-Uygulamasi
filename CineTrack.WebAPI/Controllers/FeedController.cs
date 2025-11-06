using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CineTrack.WebAPI.Services;

namespace CineTrack.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedController : ControllerBase
{
	private readonly FeedService _feedService;

	public FeedController(FeedService feedService)
	{
		_feedService = feedService;
	}

	// Takip ettiklerinin aktiviteleri (login gerekli)
	[Authorize]
	[HttpGet]
	public async Task<IActionResult> GetFeed()
	{
		var feed = await _feedService.GetFeedAsync();
		return Ok(feed);
	}

	// Belirli bir kullanıcının aktiviteleri
	[HttpGet("user/{id}")]
	public async Task<IActionResult> GetUserActivities(int id)
	{
		var activities = await _feedService.GetUserActivitiesAsync(id);
		return Ok(activities);
	}
}

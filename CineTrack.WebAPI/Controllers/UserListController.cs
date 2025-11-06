using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CineTrack.WebAPI.Services;

namespace CineTrack.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserListController : ControllerBase
{
	private readonly UserListService _listService;

	public UserListController(UserListService listService)
	{
		_listService = listService;
	}

	// 1) Liste oluştur
	[HttpPost]
	public async Task<IActionResult> CreateList([FromBody] dynamic body)
	{
		string name = body.name;
		string? desc = body.description;
		var list = await _listService.CreateListAsync(name, desc);
		return Ok(list);
	}

	// 2) Tüm listeleri getir
	[HttpGet]
	public async Task<IActionResult> GetLists()
	{
		var lists = await _listService.GetUserListsAsync();
		return Ok(lists);
	}

	// 3) Liste detay
	[HttpGet("{id}")]
	public async Task<IActionResult> GetListDetail(int id)
	{
		var list = await _listService.GetListDetailAsync(id);
		if (list == null)
			return NotFound(new { message = "Liste bulunamadi." });
		return Ok(list);
	}

	// 4) Listeye içerik ekle
	[HttpPost("{id}/add")]
	public async Task<IActionResult> AddContent(int id, [FromBody] dynamic body)
	{
		string contentId = body.contentId;
		bool result = await _listService.AddContentToListAsync(id, contentId);
		if (!result)
			return BadRequest(new { message = "Icerik eklenemedi." });

		return Ok(new { message = "Icerik listeye eklendi." });
	}

	// 5) Listeden içerik sil
	[HttpDelete("{id}/remove/{contentId}")]
	public async Task<IActionResult> RemoveContent(int id, string contentId)
	{
		bool result = await _listService.RemoveContentFromListAsync(id, contentId);
		if (!result)
			return BadRequest(new { message = "Icerik kaldirilamadi." });

		return Ok(new { message = "Icerik listeden silindi." });
	}
}

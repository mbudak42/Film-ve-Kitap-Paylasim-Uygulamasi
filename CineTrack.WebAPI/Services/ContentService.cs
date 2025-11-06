using System.Text.Json;
using CineTrack.DataAccess;
using CineTrack.DataAccess.Entities;
using CineTrack.WebAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CineTrack.WebAPI.Services;

public class ContentService
{
	private readonly CineTrackDbContext _context;
	private readonly HttpClient _httpClient;
	private readonly IConfiguration _config;

	public ContentService(CineTrackDbContext context, IConfiguration config)
	{
		_context = context;
		_config = config;
		_httpClient = new HttpClient();
	}

	// 1) Film arama
	public async Task<List<ContentDto>> SearchMoviesAsync(string query)
	{
		var apiKey = _config["ExternalApis:TMDb:ApiKey"];
		var baseUrl = _config["ExternalApis:TMDb:BaseUrl"];
		var url = $"{baseUrl}/search/movie?api_key={apiKey}&query={Uri.EscapeDataString(query)}&language=tr-TR";

		var response = await _httpClient.GetStringAsync(url);
		using var doc = JsonDocument.Parse(response);

		var results = doc.RootElement.GetProperty("results");
		var contents = new List<ContentDto>();

		foreach (var item in results.EnumerateArray())
		{
			contents.Add(new ContentDto
			{
				Id = item.GetProperty("id").GetRawText(),
				Title = item.GetProperty("title").GetString() ?? "Bilinmiyor",
				ContentType = "movie",
				CoverUrl = item.TryGetProperty("poster_path", out var poster)
					? $"https://image.tmdb.org/t/p/w500{poster.GetString()}"
					: null
			});
		}
		return contents;
	}

	// 2) Kitap arama
	public async Task<List<ContentDto>> SearchBooksAsync(string query)
	{
		var baseUrl = _config["ExternalApis:GoogleBooks:BaseUrl"];
		var url = $"{baseUrl}?q={Uri.EscapeDataString(query)}";

		var response = await _httpClient.GetStringAsync(url);
		using var doc = JsonDocument.Parse(response);

		IEnumerable<JsonElement> items;

		if (doc.RootElement.TryGetProperty("items", out var arr))
			items = arr.EnumerateArray();
		else
			items = Array.Empty<JsonElement>();

		var contents = new List<ContentDto>();

		foreach (var item in items)
		{
			var volumeInfo = item.GetProperty("volumeInfo");
			var id = item.GetProperty("id").GetString() ?? Guid.NewGuid().ToString();
			var title = volumeInfo.GetProperty("title").GetString() ?? "Bilinmiyor";
			var cover = volumeInfo.TryGetProperty("imageLinks", out var link) && link.TryGetProperty("thumbnail", out var img)
				? img.GetString()
				: null;

			contents.Add(new ContentDto
			{
				Id = id,
				Title = title,
				ContentType = "book",
				CoverUrl = cover
			});
		}
		return contents;
	}

	// 3) İçerik detayını getir (önce DB'den, yoksa API'den)
	public async Task<ContentDto?> GetContentAsync(string id, string type)
	{
		var content = await _context.Contents.FirstOrDefaultAsync(c => c.Id == id);
		if (content != null)
		{
			return new ContentDto
			{
				Id = content.Id,
				Title = content.Title,
				ContentType = content.ContentType,
				CoverUrl = content.CoverUrl,
				MetadataJson = content.MetadataJson
			};
		}

		// Harici API’den çek
		if (type == "movie")
			return await FetchAndSaveMovieAsync(id);
		else if (type == "book")
			return await FetchAndSaveBookAsync(id);

		return null;
	}

	// 4) Film detayını getir ve kaydet
	private async Task<ContentDto?> FetchAndSaveMovieAsync(string id)
	{
		var apiKey = _config["ExternalApis:TMDb:ApiKey"];
		var baseUrl = _config["ExternalApis:TMDb:BaseUrl"];
		var url = $"{baseUrl}/movie/{id}?api_key={apiKey}&language=tr-TR";

		var response = await _httpClient.GetStringAsync(url);
		using var doc = JsonDocument.Parse(response);
		var root = doc.RootElement;

		var title = root.GetProperty("title").GetString()!;
		var cover = root.TryGetProperty("poster_path", out var poster)
			? $"https://image.tmdb.org/t/p/w500{poster.GetString()}"
			: null;

		var content = new Content
		{
			Id = id,
			Title = title,
			ContentType = "movie",
			CoverUrl = cover,
			MetadataJson = response
		};

		_context.Contents.Add(content);
		await _context.SaveChangesAsync();

		return new ContentDto
		{
			Id = id,
			Title = title,
			ContentType = "movie",
			CoverUrl = cover,
			MetadataJson = response
		};
	}

	// 5) Kitap detayını getir ve kaydet
	private async Task<ContentDto?> FetchAndSaveBookAsync(string id)
	{
		var baseUrl = _config["ExternalApis:GoogleBooks:BaseUrl"];
		var url = $"{baseUrl}/{id}";

		var response = await _httpClient.GetStringAsync(url);
		using var doc = JsonDocument.Parse(response);
		var info = doc.RootElement.GetProperty("volumeInfo");

		var title = info.GetProperty("title").GetString() ?? "Bilinmiyor";
		var cover = info.TryGetProperty("imageLinks", out var link) && link.TryGetProperty("thumbnail", out var img)
			? img.GetString()
			: null;

		var content = new Content
		{
			Id = id,
			Title = title,
			ContentType = "book",
			CoverUrl = cover,
			MetadataJson = response
		};

		_context.Contents.Add(content);
		await _context.SaveChangesAsync();

		return new ContentDto
		{
			Id = id,
			Title = title,
			ContentType = "book",
			CoverUrl = cover,
			MetadataJson = response
		};
	}
}

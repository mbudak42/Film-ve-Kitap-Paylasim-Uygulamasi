using CineTrack.MvcUI.Models;
using CineTrack.MvcUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CineTrack.MvcUI.Controllers;

public class SearchController : Controller
{
    private readonly ApiService _api;

    public SearchController(ApiService api)
    {
        _api = api;
    }

    // GET: /Search
    public async Task<IActionResult> Index(string? query, string? contentType, string? genre, int? year, double? minRating)
    {
        var model = new SearchViewModel
        {
            Query = query,
            ContentType = contentType ?? "all",
            Genre = genre,
            Year = year,
            MinRating = minRating
        };

        try
        {
            // Türleri yükle
            var genres = await _api.GetAsync<List<string>>("content/genres");
            if (genres != null)
            {
                model.AvailableGenres = genres;
            }

            // Eğer arama yapıldıysa
            if (!string.IsNullOrWhiteSpace(query))
            {
                var searchParams = BuildSearchQuery(query, contentType, genre, year, minRating);
                var searchResult = await _api.GetAsync<SearchResultDto>($"content/search?{searchParams}");
                
                if (searchResult != null)
                {
                    model.SearchResults = searchResult.Results;
                }
            }
            else
            {
                // Arama yapılmadıysa vitrinleri göster
                await LoadShowcases(model);
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = "Veriler yüklenirken bir hata oluştu.";
            Console.WriteLine($"Search Error: {ex.Message}");
        }

        return View(model);
    }

    // AJAX ile arama sonuçlarını getir
    [HttpGet]
    public async Task<IActionResult> SearchPartial(string query, string? contentType, string? genre, int? year, double? minRating)
    {
        try
        {
            var searchParams = BuildSearchQuery(query, contentType, genre, year, minRating);
            var searchResult = await _api.GetAsync<SearchResultDto>($"content/search?{searchParams}");

            if (searchResult == null)
            {
                return PartialView("_SearchResults", new List<ContentCardDto>());
            }

            return PartialView("_SearchResults", searchResult.Results);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Search Error: {ex.Message}");
            return PartialView("_SearchResults", new List<ContentCardDto>());
        }
    }

    // Vitrinleri yükle (En Yüksek Puanlılar, En Popülerler)
    private async Task LoadShowcases(SearchViewModel model)
    {
        try
        {
            // En Yüksek Puanlılar
            var topRated = await _api.GetAsync<List<ContentCardDto>>("content/top-rated?limit=12");
            if (topRated != null)
            {
                model.TopRated = topRated;
            }

            // En Popülerler
            var mostPopular = await _api.GetAsync<List<ContentCardDto>>("content/most-popular?limit=12");
            if (mostPopular != null)
            {
                model.MostPopular = mostPopular;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Showcase Error: {ex.Message}");
        }
    }

    // Arama parametrelerini query string'e çevir
    private string BuildSearchQuery(string? query, string? contentType, string? genre, int? year, double? minRating)
    {
        var parameters = new List<string>();

        if (!string.IsNullOrWhiteSpace(query))
            parameters.Add($"query={Uri.EscapeDataString(query)}");

        if (!string.IsNullOrWhiteSpace(contentType) && contentType != "all")
            parameters.Add($"type={contentType}");

        if (!string.IsNullOrWhiteSpace(genre))
            parameters.Add($"genre={Uri.EscapeDataString(genre)}");

        if (year.HasValue)
            parameters.Add($"year={year}");

        if (minRating.HasValue)
            parameters.Add($"minRating={minRating}");

        return string.Join("&", parameters);
    }
}
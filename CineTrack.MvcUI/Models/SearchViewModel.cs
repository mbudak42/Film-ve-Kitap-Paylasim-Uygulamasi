using System.ComponentModel.DataAnnotations;

namespace CineTrack.MvcUI.Models;

// Arama için kullanılacak ViewModel
public class SearchViewModel
{
    public string? Query { get; set; }
    
    public string? ContentType { get; set; } // "movie", "book", "all"
    
    public string? Genre { get; set; }
    
    public int? Year { get; set; }
    
    public double? MinRating { get; set; }
    
    public List<ContentCardDto> SearchResults { get; set; } = new();
    
    public List<ContentCardDto> TopRated { get; set; } = new();
    
    public List<ContentCardDto> MostPopular { get; set; } = new();
    
    public List<string> AvailableGenres { get; set; } = new();
}

// Arama sonuçları için kart modeli
public class ContentCardDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty; // "movie" veya "book"
    public string? PosterUrl { get; set; }
    public int? Year { get; set; }
    public double? AverageRating { get; set; }
    public int RatingCount { get; set; }
    public List<string> Genres { get; set; } = new();
    public string? Director { get; set; } // Filmler için
    public string? Author { get; set; } // Kitaplar için
}

// API'den gelecek arama sonucu
public class SearchResultDto
{
    public List<ContentCardDto> Results { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
}
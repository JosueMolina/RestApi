using System.Text.RegularExpressions;

namespace Movies.Application.Models;

public partial class Movie
{
  public required Guid Id { get; init; }
  public required string Title { get; set; }
  public string Slug => GenerateSlug();
  public required int YearOfRelease { get; set; }
  public required IList<string> Genres { get; init; } = [];
  
  private string GenerateSlug()
  {
    var sluggedTitle = SlugRegex().Replace(Title, string.Empty)
      .Trim()
      .Replace(" ", "-")
      .ToLower();

    return $"{sluggedTitle}-{YearOfRelease}";
  }

  [GeneratedRegex("[^0-9A-Za-z _-]", RegexOptions.NonBacktracking, 5)]
  private static partial Regex SlugRegex();
}
using FluentValidation;
using Movies.Application.Models;
using Movies.Application.Repositories;

namespace Movies.Application.Services;

public class MovieService : IMovieService
{
  private readonly IMovieRepository _movieRepository;
  private readonly IValidator<Movie> _movieValidator;

  public MovieService(IMovieRepository movieRepository, IValidator<Movie> movieValidator)
  {
    _movieRepository = movieRepository;
    _movieValidator = movieValidator;
  }

  public async Task<bool> CreateMovieAsync(Movie movie, CancellationToken token = default)
  {
    await _movieValidator.ValidateAndThrowAsync(movie, cancellationToken: token);
    return await _movieRepository.CreateMovieAsync(movie, token);
  }

  public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken token = default)
  {
    return await _movieRepository.GetByIdAsync(id, token);
  }

  public async Task<Movie?> GetBySlugAsync(string slug, CancellationToken token = default)
  {
    return await _movieRepository.GetBySlugAsync(slug, token);
  }

  public async Task<IEnumerable<Movie>> GetAllAsync(CancellationToken token = default)
  {
    return await _movieRepository.GetAllAsync(token);
  }

  public async Task<Movie?> UpdateAsync(Movie movie, CancellationToken token = default)
  {
    var exists = await _movieRepository.ExistsByIdAsync(movie.Id, token);
    if (!exists)
    {
      return null;
    }

    await _movieRepository.UpdateAsync(movie, token);
    return movie;
  }

  public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken token = default)
  {
    return await _movieRepository.DeleteByIdAsync(id, token);
  }
}
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

  public async Task<bool> CreateMovieAsync(Movie movie)
  {
    await _movieValidator.ValidateAndThrowAsync(movie);
    return await _movieRepository.CreateMovieAsync(movie);
  }

  public async Task<Movie?> GetByIdAsync(Guid id)
  {
    return await _movieRepository.GetByIdAsync(id);
  }

  public async Task<Movie?> GetBySlugAsync(string slug)
  {
    return await _movieRepository.GetBySlugAsync(slug);
  }

  public async Task<IEnumerable<Movie>> GetAllAsync()
  {
    return await _movieRepository.GetAllAsync();
  }

  public async Task<Movie?> UpdateAsync(Movie movie)
  {
    var exists = await _movieRepository.ExistsByIdAsync(movie.Id);
    if (!exists)
    {
      return null;
    }

    await _movieRepository.UpdateAsync(movie);
    return movie;
  }

  public async Task<bool> DeleteByIdAsync(Guid id)
  {
    return await _movieRepository.DeleteByIdAsync(id);
  }
}
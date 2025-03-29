using Microsoft.AspNetCore.Mvc;
using Movies.API.Mapping;
using Movies.Application.Repositories;
using Movies.Contracts.Requests;
using Movies.Contracts.Responses;

namespace Movies.API.Controllers;

[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MoviesController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    [HttpPost(ApiEndpoints.Movies.Create)]
    public async Task<IActionResult> Create([FromBody] CreateMovieRequest request)
    {
        var movie = request.MapToMovie();
        await _movieRepository.CreateMovieAsync(movie);

        var response = movie.MapToMovieResponse();
        
        return CreatedAtAction(nameof(Get), new { id = movie.Id }, response);
    }

    [HttpGet(ApiEndpoints.Movies.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        
        if (movie is null)
        {
            return NotFound();
        }

        var response = movie.MapToMovieResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Movies.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _movieRepository.GetAllAsync();
        var response = movies.MapToResponse();
        return Ok(response);
    }
}
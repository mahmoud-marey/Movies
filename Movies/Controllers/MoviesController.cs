using System.Xml.Linq;

namespace Movies.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly IMoviesService _moviesService;
    private readonly IGenresService _genreService;
    private readonly IMapper _mapper;
    private readonly List<string> _allowedExtensions = new() { ".jpeg", ".png", ".jpg" };
    private readonly long _maxAllowedSize = 1 * 1024 * 1024;

    public MoviesController(IMoviesService moviesService, IGenresService genreService, IMapper mapper)
    {
        _moviesService = moviesService;
        _genreService = genreService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var movies = await _moviesService.GetAllAsync();

        var moviesDto = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

        return Ok(moviesDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] MovieCreateDto dto)
    {

        var fileExtension = Path.GetExtension(dto.Poster.FileName).ToLower();
        if (!_allowedExtensions.Contains(fileExtension))
            return BadRequest("Only jpeg, jpg, png images are allowed");

        if (dto.Poster.Length > _maxAllowedSize)
            return BadRequest($"Max allowed size is {_maxAllowedSize / 1024 / 1024}MB");

        var isValidGenre = await _genreService.IsExistsAsync(dto.GenreId);
        if (!isValidGenre)
            return BadRequest("Invalid genre Id");

        var movie = _mapper.Map<Movie>(dto);

        await _moviesService.CreateAsync(movie);
        //the next line is to populate the movie obj with it's genre (navigation property)
        movie = await _moviesService.GetByIdAsync(movie.Id);

        var movieDetailsDto = _mapper.Map<MovieDetailsDto>(movie);

        return Ok(movieDetailsDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieByIdAsync(int id)
    {
        var movie = await _moviesService.GetByIdAsync(id);

        if (movie is null)
            return NotFound($"No movie with Id :{id}");

        var dto = _mapper.Map<MovieDetailsDto>(movie);

        return Ok(dto);
    }

    [HttpGet("getByTitle")]
    public IActionResult GetMovieByTitle([FromQuery]string title)
    {
        var movies = _moviesService.GetByTitle(title);

        if (movies is null)
            return NotFound($"No movie title contains: {title}");

        var dto = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

        return Ok(dto);
    }

    [HttpGet("getByYear")]
    public async Task<IActionResult> GetByNameAsync([FromQuery] int year)
    {
        var movies = _moviesService.GetByYear(year);

        if (movies is null)
            return NotFound($"No movie in: {year}");

        var dto = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

        return Ok(dto);
    }

    [HttpGet("getByGenre/{id}")]
    public async Task<IActionResult> GetByGenreIdAsync([FromRoute] byte id)
    {
        var genreExists = await _genreService.IsExistsAsync(id) ;
        if (!genreExists)
            return NotFound($"No genre with Id: {id}");

        var movies = _moviesService.GetByGenreId(id);
        if (!movies.Any())
            return BadRequest($"No movies assigned to genre Id:{id}");

        var moviesDto = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

        return Ok(moviesDto);

    }

    [HttpGet("getByGenre")]
    public async Task<IActionResult> GetByGenreNameAsync([FromQuery] string name)
    {
        var genre = await _genreService.GetByNameAsync(name);
        if (genre is null)
            return NotFound($"No genre with name: {name}");

        var movies = _moviesService.GetByGenreName(name);
        if (!movies.Any())
            return BadRequest($"No movies assigned to genre name: {name}");

        var moviesDto = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

        return Ok(moviesDto);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] MovieUpdateDto dto)
    {
        var movie = await _moviesService.GetByIdAsync(id);
        if (movie is null)
            return NotFound($"There is no movie with the ID:{id}");

        movie = _mapper.Map(dto,movie);

        if (dto.Poster is not null)
            movie.Poster = Converter.IFormFileToByteArray(dto.Poster);

        _moviesService.Update(movie);
        // to add genre to the movie after it is updated
        movie = await _moviesService.GetByIdAsync(movie.Id);

        var movieDto = _mapper.Map<MovieDetailsDto>(movie);

        return Ok(movieDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var movie = await _moviesService.GetByIdAsync(id);
        if (movie is null)
            return BadRequest($"No movie with Id: {id}");

        _moviesService.Delete(movie);

        var movieDto = _mapper.Map<MovieDetailsDto>(movie);


        return Ok(movieDto);
    }
}

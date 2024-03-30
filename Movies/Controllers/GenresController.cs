namespace Movies.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IGenresService _genresService;
    private readonly IMapper _mapper;

    public GenresController(IGenresService genresService, IMapper mapper)
    {
        _genresService = genresService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var genres = await _genresService.GetAllAsync();

        var Detailsdto = _mapper.Map<IEnumerable<GenreDetailsDto>>(genres);

        return Ok(Detailsdto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(byte id)
    {
        var genre = await _genresService.GetByIdAsync(id);

        if (genre is null)
            return NotFound($"There is no Genre with Id: {id}");

        var Detailsdto = _mapper.Map<GenreDetailsDto>(genre);

        return Ok(Detailsdto);
    }

    [HttpGet("getByName")]
    public async Task<IActionResult> GetByNameAsync([FromQuery] string name)
    {
        var genre = await _genresService.GetByNameAsync(name);

        if(genre is null)
            return NotFound($"There is no Genre with name: {name}");

        var Detailsdto = _mapper.Map<GenreDetailsDto>(genre);

        return Ok(Detailsdto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody]GenreDto dto)
    {
        var genre = _mapper.Map<Genre>(dto);
        genre = await _genresService.CreateAsync(genre);
        var DetailsDto = _mapper.Map<GenreDetailsDto>(genre);
        return Ok(DetailsDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] byte id ,[FromBody] GenreDto dto)
    {
        var genre = await _genresService.GetByIdAsync(id);

        if (genre is null)
            return BadRequest($"There is no Genre with ID: {id}");
        genre = _mapper.Map(dto,genre);
        genre = _genresService.Update(genre);
        var DetailsDto = _mapper.Map<GenreDetailsDto>(genre);
        return Ok(DetailsDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] byte id)
        {
        var genre = await _genresService.GetByIdAsync(id);

        if (genre is null)
            return NotFound($"There is no Genre with ID:{id}");

        genre = _genresService.Delete(genre);
        var DetailsDto = _mapper.Map<GenreDetailsDto>(genre);
        return Ok(DetailsDto);
    }
}

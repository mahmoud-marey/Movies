namespace Movies;
public class MovieCreateDto : MovieBase
{
    [JsonPropertyOrder(1)]
    public IFormFile Poster { get; set; } = null!;
}
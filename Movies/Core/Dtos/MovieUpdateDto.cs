namespace Movies.Core.Dtos;
public class MovieUpdateDto : MovieBase
{
    [JsonPropertyOrder(1)]
    public IFormFile? Poster { get; set; } = null!;
}

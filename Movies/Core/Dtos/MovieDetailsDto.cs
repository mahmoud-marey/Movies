namespace Movies.Core.Dtos;
public class MovieDetailsDto : MovieBase
{
    public int Id { get; set; }
    [JsonPropertyOrder(2)]
    public byte[] Poster { get; set; } = null!;
    [JsonPropertyOrder(1)]
    public string GenreName { get; set; } = null!;
}

namespace Movies.Core.Models;
public class Movie : MovieBase
{
    public int Id { get; set; }
    public byte[] Poster { get; set; } = null!;
    public Genre? Genre { get; set; }
}

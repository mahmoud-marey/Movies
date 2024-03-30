namespace Movies.Core.Base;
public abstract class MovieBase
{
    [MaxLength(150)]
    public string Title { get; set; } = null!;
    [MaxLength(2500)]
    public string StoreLine { get; set; } = null!;
    public decimal Rate { get; set; }
    public int Year { get; set; }
    public byte GenreId { get; set; }
}

namespace Movies.Services;
public interface IMoviesService
{
    public Task<IEnumerable<Movie>> GetAllAsync();
    public Task<Movie> GetByIdAsync(int id);
    public IEnumerable<Movie> GetByTitle(string title);
    public IEnumerable<Movie> GetByGenreId(short id);
    public IEnumerable<Movie> GetByGenreName(string name);
    public IEnumerable<Movie> GetByYear(int year);
    public Task<Movie> CreateAsync(Movie movie);
    public Movie Update(Movie movie);
    public Movie Delete(Movie movie);
}

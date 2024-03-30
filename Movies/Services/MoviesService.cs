namespace Movies.Services;

public class MoviesService : IMoviesService
{
    private readonly ApplicationDbContext _context;

    public MoviesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        var movies = await _context.Movies
                                 .OrderByDescending(x => x.Rate)
                                 .Include(m => m.Genre)
                                 .AsNoTracking()
                                 .ToListAsync();

        return movies;
    }
    public async Task<Movie> CreateAsync(Movie movie)
    {
        
        await _context.AddAsync(movie);
        _context.SaveChanges();
        return movie;
    }

    public  Movie Delete(Movie movie)
    {
        _context.Remove(movie);
        _context.SaveChanges();

        return movie;
    }

    public async Task<Movie> GetByIdAsync(int id)
    {
        var movie = await _context.Movies
                                  .Include(m => m.Genre)
                                  .SingleOrDefaultAsync(m => m.Id == id);

        return movie!;
    }

    public Movie Update(Movie movie)
    {
        _context.Update(movie);
        _context.SaveChanges();
        return movie ;
    }

    public IEnumerable<Movie> GetByGenreId(short id)
    {
        var movies = _context.Movies
                             .Include(m => m.Genre)
                             .Where(m => m.GenreId == id)
                             .AsNoTracking()
                             .OrderByDescending(x => x.Rate);
        return movies;
    }

    public IEnumerable<Movie> GetByGenreName(string name)
    {
        var movies = _context.Movies
                             .Include(m => m.Genre)
                             .Where(m => m.Genre!.Name == name)
                             .AsNoTracking()
                             .OrderByDescending(x => x.Rate);
        return movies;
    }

    public IEnumerable<Movie> GetByTitle(string title) =>
        _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.Title.Contains(title))
                .AsNoTracking()
                .OrderByDescending(x => x.Rate);
        
    

    public IEnumerable<Movie> GetByYear(int year) =>
        _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.Year == year)
                .AsNoTracking()
                .OrderByDescending(x => x.Rate);
}

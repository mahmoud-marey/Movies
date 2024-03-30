namespace Movies.Services;

public class GenresService : IGenresService
{
    private readonly ApplicationDbContext _context;

    public GenresService(ApplicationDbContext context) =>
        _context = context;

    public async Task<List<Genre>> GetAllAsync() =>
        await _context.Genres.AsNoTracking().ToListAsync();

    public async Task<Genre> GetByIdAsync(byte id) =>
        await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

    public async Task<Genre> GetByNameAsync(string name) =>
        await _context.Genres.SingleOrDefaultAsync(g => g.Name == name)!;

    public async Task<Genre> CreateAsync(Genre genre)
    {
        await _context.Genres.AddAsync(genre);
        await _context.SaveChangesAsync();

        return genre;

    }
    public Genre Update(Genre genre)
    {
        _context.Genres.Update(genre);
        _context.SaveChanges();

        return genre;
    }
    public Genre Delete(Genre genre)
    {
        _context.Genres.Remove(genre);
        _context.SaveChanges();
        return genre;
    }
    public Task<bool> IsExistsAsync(byte id) =>
        _context.Genres.AnyAsync(g => g.Id == id);

}

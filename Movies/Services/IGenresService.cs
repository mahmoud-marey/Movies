namespace Movies.Services;

public interface IGenresService
{
    public Task<List<Genre>> GetAllAsync();
    public Task<Genre> GetByIdAsync(byte id);
    public Task<Genre> GetByNameAsync(string name);
    public Task<Genre> CreateAsync(Genre Genre);
    public Genre Update(Genre genre);
    public Genre Delete(Genre genre);
    public Task<bool> IsExistsAsync(byte id);
}

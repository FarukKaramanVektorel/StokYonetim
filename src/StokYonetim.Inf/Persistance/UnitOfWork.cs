using StokYonetim.Application.Interfaces;
using StokYonetim.Inf.Persistance;

namespace StokYonetim.Inf.Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly StokDbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(StokDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            var repositoryInstance = new Repository<T>(_context);
            _repositories[type] = repositoryInstance;
        }
        return (IRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}

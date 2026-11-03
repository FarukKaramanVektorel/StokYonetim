using System;
using System.Collections.Generic;
using System.Text;

namespace StokYonetim.Application.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

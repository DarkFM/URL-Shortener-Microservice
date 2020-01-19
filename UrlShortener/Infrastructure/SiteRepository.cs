using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Domain.Entities;
using UrlShortener.Domain.Repositories;

namespace UrlShortener.Infrastructure
{
    public class SiteRepository : ISiteRepository
    {
        private readonly AppDbContext _dbContext;

        public SiteRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public Website Add(Website site)
        {
            return _dbContext.Sites.Add(site).Entity;
        }

        public async Task<Website> GetByIdAsync(int id)
        {
            return await _dbContext.Sites
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Website> GetByUrl(string url)
        {
            return await _dbContext.Sites
                .AsNoTracking()
                .SingleOrDefaultAsync(w => w.Url.ToLower() == url.ToLower());
        }
    }
}

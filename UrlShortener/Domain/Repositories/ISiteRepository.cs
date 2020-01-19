using System.Threading.Tasks;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Domain.Repositories
{
    public interface ISiteRepository : IRepository
    {
        Website Add(Website site);
        Task<Website> GetByIdAsync(int id);
        Task<Website> GetByUrl(string url);
    }
}

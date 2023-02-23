using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Models;

namespace UrlShortener.Data.Repositories.Interfaces
{
    public interface IUrlRepository : IDisposable
    {
        Task CreateShortUrlAsync(UrlPairDomainModel pair, ulong next);
        Task<IEnumerable<UrlPair>> GetAllUrlPairsAsync();
        Task<UrlPair> GetUrlPairByLongUrlAsync(string longUrl);
        Task<UrlPair> GetUrlPairByShortCodeAsync(string shortUrlCode);
        Task<ulong> GetLastNumericId();
    }
}

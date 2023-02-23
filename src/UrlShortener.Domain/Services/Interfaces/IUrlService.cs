using System.Net.NetworkInformation;
using UrlShortener.Data.Models;

namespace UrlShortener.Domain.Services.Interfaces
{
    public interface IUrlService
    {
        Task<string> CreateShortUrlAsync(string longUrl);
        Task<IEnumerable<UrlPairDomainModel>> GetAllUrlPairsAsync();
        Task<string> GetLongUrlAsync(string shortUrlCode);
        bool IsCodeStructureValid(string shortUrlCode);
    }
}
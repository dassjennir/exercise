using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Models;
using UrlShortener.Data.Repositories.Interfaces;

namespace UrlShortener.Data.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlShortener.Data.UrlContext _context;
        private readonly DbSet<UrlPair> _set;
        private bool disposedValue;

        public UrlRepository(UrlShortener.Data.UrlContext context)
        {
            _context = context;
            _set = _context.UrlPairs;
        }

        public async Task CreateShortUrlAsync(UrlPairDomainModel urlPair, ulong next)
        {
            _set.Add(new UrlPair
            {
                LongUrl = urlPair.LongUrl,
                ShortUrlCode = urlPair.ShortUrlCode,
                NumericIndex = next
            });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UrlPair>> GetAllUrlPairsAsync()
        {
            return await _set.ToListAsync();
        }

        public Task<UrlPair> GetUrlPairByLongUrlAsync(string longUrl)
        {
            return _set.FirstOrDefaultAsync(p => p.LongUrl == longUrl);
        }

        public Task<UrlPair> GetUrlPairByShortCodeAsync(string shortUrlCode)
        {
            return _set.FirstOrDefaultAsync(p => p.ShortUrlCode == shortUrlCode);
        }

        public async Task<ulong> GetLastNumericId()
        {
            var pair = await _set.LastAsync();
            return pair?.NumericIndex ?? 0; // in case of empty db
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        ~UrlRepository()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


    }
}

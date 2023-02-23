using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NumeralSystemConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using UrlShortener.Data;
using UrlShortener.Data.Models;
using UrlShortener.Data.Repositories.Interfaces;
using UrlShortener.Domain.Exceptions;
using UrlShortener.Domain.Services.Interfaces;

namespace UrlShortener.Domain.Services
{
    public class UrlService : IUrlService
    {
        private readonly ILogger<UrlService> _logger;
        private readonly IUrlRepository _repository;
        private readonly IConfiguration _config;

        public UrlService(ILogger<UrlService> logger, IUrlRepository repository, IConfiguration config)
        {
            _logger = logger;
            _repository = repository;
            _config = config;
        }

        public async Task<string> CreateShortUrlAsync(string longUrl)
        {
            UrlPairDomainModel pairDomain;
            UrlPair pair = await _repository.GetUrlPairByLongUrlAsync(longUrl);
            if (pair == null)
            {
                try
                {
                    ulong last = await _repository.GetLastNumericId();
                    pairDomain = GenerateNewLongShortUrlPair(longUrl, last);
                    await _repository.CreateShortUrlAsync(pairDomain, last+1);
                    _logger.LogInformation($"Created new short URL for {longUrl}. Returned {pairDomain.ShortUrlCode}.");
                    return pairDomain.ShortUrlCode;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    _logger.LogInformation(ex.Message);
                    throw;
                }
            }
            else
            {
                pairDomain = new UrlPairDomainModel(pair);
                _logger.LogInformation($"Short URL for {longUrl} already existed. Returned {pair.ShortUrlCode}");
                return pairDomain.ShortUrlCode;
            }
        }

        private UrlPairDomainModel GenerateNewLongShortUrlPair(string longUrl, ulong last)
        {
            string shortCode = GenerateNextShortUrlCode(last);
            var pairDomain = new UrlPairDomainModel
            {
                LongUrl = longUrl,
                ShortUrlCode = shortCode
            };
            return pairDomain;
        }

        public async Task<IEnumerable<UrlPairDomainModel>> GetAllUrlPairsAsync()
        {
            IEnumerable<UrlPairDomainModel> pairs = (await _repository.GetAllUrlPairsAsync()).Select(p => new UrlPairDomainModel(p));
            return pairs;
        }

        public async Task<string> GetLongUrlAsync(string shortUrlCode)
        {
            UrlPair pair = await _repository.GetUrlPairByShortCodeAsync(shortUrlCode);
            return pair?.LongUrl;
        }

        public bool IsCodeStructureValid(string shortUrlCode)
        {
            if (shortUrlCode.Any(c => !char.IsLetter(c))) return false;
            return true;
        }

        private string GenerateNextShortUrlCode(ulong last)
        {
            return Converter.ConvertFromDecimal(last + 1);
        }
    }
}

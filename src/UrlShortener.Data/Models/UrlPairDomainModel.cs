using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Data.Models
{
    public class UrlPairDomainModel
    {
        public UrlPairDomainModel() { }
        public UrlPairDomainModel(UrlPair entity)
        {
            LongUrl = entity.LongUrl;
            ShortUrlCode = entity.ShortUrlCode;
        }

        public string LongUrl { get; init; }
        //public string ShortUrl { get; }
        public string ShortUrlCode { get; init; }
    }
}

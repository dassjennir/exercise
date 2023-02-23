using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Models.Interfaces;

namespace UrlShortener.Data.Models
{
    public class UrlPair : IEntity
    {
        public Guid Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrlCode { get; set; } // no ShortUrl here to make possibility to change short URL domain. Present in domain model.
        public ulong NumericIndex { get; set; }
    }
}

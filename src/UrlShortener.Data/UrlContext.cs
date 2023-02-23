using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data.Models;

namespace UrlShortener.Data
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options) : base(options) { }
        public DbSet<UrlPair> UrlPairs { get; set; }
    }
}

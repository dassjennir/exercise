using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Domain.Exceptions
{
    public class ShortUrlLimitOverflowException : Exception
    {
        public override string Message => $"Limit exceeded. Cannot create more URLs";
    }
}

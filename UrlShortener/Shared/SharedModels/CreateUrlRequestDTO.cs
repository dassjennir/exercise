using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Shared.SharedModels
{
    public class CreateUrlRequestDTO
    {
        [Url(ErrorMessage = "Inputed URL is not a proper URL.")]
        [MaxLength(300, ErrorMessage = "Limit 300 characters exceeded.")]
        public string LongUrl { get; set; }
    }
}

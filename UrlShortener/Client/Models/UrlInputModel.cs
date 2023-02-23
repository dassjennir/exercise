using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Client.Models
{
    public class UrlInputModel
    {
        [Required(ErrorMessage = "Please input URL in the form.")]
        [Url(ErrorMessage = "Inputed text must be a proper URL.")]
        [MaxLength(300, ErrorMessage = "Cannot exceed 300 characters.")]
        public string Url { get; set; }
    }
}

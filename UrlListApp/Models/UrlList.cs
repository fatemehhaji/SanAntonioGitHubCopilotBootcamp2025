using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UrlListApp.Models
{
    public class UrlList
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? CustomUrl { get; set; }
        public string? GeneratedUrl { get; set; }
        public bool IsPublished { get; set; }
        public virtual List<UrlItem> Urls { get; set; } = new();
    }
}

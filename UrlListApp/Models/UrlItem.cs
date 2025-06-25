using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlListApp.Models
{
    public class UrlItem
    {
        public int Id { get; set; }
        [Required]
        [Url]
        public string Url { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int UrlListId { get; set; }
        [ForeignKey("UrlListId")]
        public virtual UrlList? UrlList { get; set; }
    }
}

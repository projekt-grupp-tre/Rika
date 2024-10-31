using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RikaWebApp.ViewModels
{
    public class ReviewViewModel
    {
        [Key]
        public int ReviewId { get; set; }

        public string ClientName { get; set; } = null!;

        //[Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ProductViewModel Product { get; set; } = null!;
    }
}
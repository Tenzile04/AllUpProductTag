using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AllUpTask.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Title1 { get; set; }
        [Required]
        [StringLength(maximumLength: 200)]
        public string Title2 { get; set; }
        [Required]
        [StringLength(maximumLength: 600)]
        public string Description { get; set; }
        public string? Image {get;set;}
        public string RedirectUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageUrl { get; set; }
    }
}

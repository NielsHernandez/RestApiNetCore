using System.ComponentModel.DataAnnotations;

namespace NZWalks.api.Models.DTO
{
    public class UpdateRegionRequestDto
    {

        [Required]
        [MinLength(3, ErrorMessage = "required 3 characters")]
        [MaxLength(3, ErrorMessage = "required 3 characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "maximon characters allow 100")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Stock
{
    public class UpdateStockRequestDto
    {

        [Required]
        [MaxLength(10, ErrorMessage = "symbol must be under 10 characters ")]
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 10000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage = "Industry must be under 10 characters ")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
